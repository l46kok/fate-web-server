using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Fate.Common.Data;
using Newtonsoft.Json;
using NLog;
using System.Timers;
using Fate.Common.Extension;
using Fate.Common.Utility;
using Fate.DB.DAL.FRS;
using Fate.DB.DAL.GHost;
using Timer = System.Timers.Timer;
#pragma warning disable 169

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
    public class GhostCommSL
    {
        private const int GHOST_FRS_PORT = 6302;
#if (!DEBUG)
        private const string GHOST_CONNECT_IP = "54.210.38.182";
        private const string GHOST_CONNECT_IP_EU = "52.210.121.172";
        private const string GHOST_CONNECT_IP_ASIA = "13.112.46.237";
        private const string GHOST_CONNECT_IP_CN = "182.18.22.91";
#else
        private const string GHOST_CONNECT_TEST_IP = "54.210.38.182";
#endif

        private const int RECONNECT_TIMER_INTERVAL = 3600 * 1000; // 1800 seconds, 30 minutes
        private const int TIMEBAN_TIMER_INTERVAL = 60 * 1000; // 60 seconds
        private static Timer _reconnectTimer;
        private static Timer _timebanTimer;

        private const int POLL_DURATION = 2 * 1000 * 1000;
        private const string GET_GAMES_COMMAND = "GetGames";
        private const string REFRESH_BAN_LIST = "RefreshBanList";
        private readonly List<SocketData> _socketList = new List<SocketData>();
        private readonly Logger _logger;

        private static readonly BanDAL _banDal = new BanDAL();
        private static readonly AdminBanSL _adminBanSl = new AdminBanSL();
        //private static readonly GHostPlayerDAL _ghostPlayerDal = new GHostPlayerDAL();

        public static GhostCommSL Instance { get; } = new GhostCommSL();

        private class SocketData
        {
            public Socket Socket { get; set; }
            public string Server { get; set; }
            public int DataLength { get; set; }
            public int RemainingData { get; set; }
            public GameListData CachedGameListData { get; set; }
        }

        private GhostCommSL()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _reconnectTimer = new Timer { Interval = RECONNECT_TIMER_INTERVAL };
            _reconnectTimer.Elapsed += ReconnectTimer_Elapsed;
            _reconnectTimer.Enabled = true;
            _reconnectTimer.Start();

            _timebanTimer = new Timer { Interval = TIMEBAN_TIMER_INTERVAL };
            _timebanTimer.Elapsed += TimebanTimer_Elapsed; ;
            _timebanTimer.Enabled = true;
            _timebanTimer.Start();

            ConnectAllSockets();
        }

        

        private void ConnectAllSockets()
        {
            _socketList.Clear();
#if (!DEBUG)
            ConnectSocket(GHOST_CONNECT_IP, GHOST_FRS_PORT, "USEast");
            // ConnectSocket(GHOST_CONNECT_IP_EU, GHOST_FRS_PORT, "Europe");
            ConnectSocket(GHOST_CONNECT_IP_ASIA, GHOST_FRS_PORT, "Asia");
            // ConnectSocket(GHOST_CONNECT_IP_CN, GHOST_FRS_PORT, "China");
#else
            // ConnectSocket(GHOST_CONNECT_TEST_IP, GHOST_FRS_PORT, "Test");
#endif
        }

        private void ReconnectTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _logger.Info("[FRS]: Reconnecting all sockets...");
            foreach (SocketData sd in _socketList)
            {
                try
                {
                    sd.Socket.Close();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "[FRS] GHost socket close error");
                }
            }
            
            ConnectAllSockets();
        }

        private void TimebanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                bool hasPlayerBeenUnbanned = false;
                foreach (var bannedPlayer in _banDal.GetCurrentlyBannedPlayers(true))
                {
                    if (bannedPlayer.BannedUntil == null)
                    {
                        _logger.Error("Error: Expected banned until duration but is null: " + bannedPlayer.PlayerName);
                        continue;
                    }

                    if (bannedPlayer.BannedUntil.Value < DateTime.Now)
                    {
                        _logger.Info("Unbanning player: " + bannedPlayer.PlayerName);
                        _adminBanSl.UnbanPlayer(bannedPlayer.PlayerName, "Timer Unban", ConfigHandler.GhostDatabaseList);

                        hasPlayerBeenUnbanned = true;
                    }
                }

                if (hasPlayerBeenUnbanned)
                {
                    RefreshBanList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[FRS] Timeban Handler Error");
            }
        }

        private bool IsSocketConnected(Socket s)
        {
            bool pollResult = s.Poll(1000, SelectMode.SelectRead);
            bool socketAvailable = (s.Available == 0);
            return !pollResult || !socketAvailable;
        }

        private void ConnectSocket(string remoteIpAddr, int port, string server)
        {
            try
            {
                //TO DO: For now, client socket uses blocking appraoch
                //Change it to Async approach later if we experience performance issue
                //Also, we're keeping the socket alive at all times for performance reason 
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(remoteIpAddr);
                //IPAddress ipAddress = null;
                //foreach (IPAddress ipAddr in ipHostInfo.AddressList)
                //{
                //    if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        ipAddress = ipAddr;
                //        break;
                //    }
                //}
                IPAddress ipAddress = IPAddress.Parse(remoteIpAddr);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                SocketData socketData = new SocketData
                {
                    Socket = socket,
                    Server = server
                };
                _socketList.Add(socketData);
                _logger.Info("Successfully Connected to FRS Socket: " + server);
            }
            catch (Exception ex)
            {
                _logger.Error(ex,"[FRS] GHost socket connection error");
            }
        }

        private void SetPlayerFateGameInfo(GameListProgressData gameProgressData)
        {
            if (gameProgressData.FRSEventList == null)
                return;
            List<string> servantSelections = gameProgressData.FRSEventList.Where(x => x.StartsWith("SS")).ToList();

            gameProgressData.Team1DataList = new List<GameListPlayerData>();
            gameProgressData.Team2DataList = new List<GameListPlayerData>();
            foreach (string s in servantSelections)
            {
                string[] splitStr = s.Split('/');
                string playerName = splitStr[1];
                int slotNum = int.Parse(splitStr[2]);
                string heroUnitTypeId = splitStr[3];
                int teamNum = int.Parse(splitStr[4]);

                if (teamNum <= 0)
                    continue;

                GameListPlayerData gpd =
                    gameProgressData.PlayerDataList.FirstOrDefault(x => string.Equals(x.PlayerName, playerName, StringComparison.InvariantCultureIgnoreCase));

                if (gpd == null)
                {
                    gpd = new GameListPlayerData
                    {
                        PlayerName = playerName,
                        Ping = "0"
                    };
                }
                gpd.SlotNumber = slotNum;
                gpd.HeroIconURL = ContentURL.GetHeroIconURL(heroUnitTypeId);
                gpd.TeamNumber = teamNum;

                GameListKDAData kdaData = gameProgressData.PlayerKills?.FirstOrDefault(x => x.PlayerID == gpd.SlotNumber);
                if (kdaData != null)
                {
                    gpd.Kills = kdaData.Kills;
                }

                GameListKDAData deathsData = gameProgressData.PlayerDeaths?.FirstOrDefault(x => x.PlayerID == gpd.SlotNumber);
                if (deathsData != null)
                {
                    gpd.Deaths = deathsData.Deaths;
                }

                GameListKDAData assistsData = gameProgressData.PlayerAssists?.FirstOrDefault(x => x.PlayerID == gpd.SlotNumber);
                if (assistsData != null)
                {
                    gpd.Assists = assistsData.Assists;
                }

                switch (gpd.TeamNumber)
                {
                    case 1:
                        gameProgressData.Team1DataList.Add(gpd);
                        break;
                    case 2:
                        gameProgressData.Team2DataList.Add(gpd);
                        break;
                }
            }

            List<string> roundVictories =
                gameProgressData.FRSEventList.Where(x => x.StartsWith("RoundVictory")).ToList();

            gameProgressData.Team1Wins = roundVictories.Count(x => x.Contains("T1"));
            gameProgressData.Team2Wins = roundVictories.Count(x => x.Contains("T2"));
        }

        public List<GameListData> GetGameList()
        {
            List<GameListData> gameListDataList = new List<GameListData>();
            bool reconnectSockets = false;
            foreach (SocketData socketData in _socketList)
            {
                try
                {
                    byte[] bytes = new byte[4096];
                    byte[] msg = Encoding.ASCII.GetBytes(GET_GAMES_COMMAND);

                    socketData.Socket.Send(msg);
                    socketData.RemainingData = int.MaxValue;
                    socketData.DataLength = 0;

                    if (socketData.Socket.Poll(POLL_DURATION, SelectMode.SelectRead))
                    {
                        GameListData gameListData;
                        int socketReadAttempt = 1000;
                        string receivedStr = "";
                        while (socketData.RemainingData > 0 && socketReadAttempt > 0)
                        {
                            int bytesRec = socketData.Socket.Receive(bytes);
                            if (socketData.DataLength <= 0)
                            {
                                // Get first 4 bytes as data length
                                byte[] length = bytes.SubArray(0, 4);
                                Array.Reverse(length);
                                socketData.DataLength = BitConverter.ToInt32(length, 0) + 4;
                                socketData.RemainingData = BitConverter.ToInt32(length, 0) + 4;
                                receivedStr += Encoding.ASCII.GetString(bytes, 4, bytesRec - 4);
                            }
                            else
                            {
                                receivedStr += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            }

                            socketData.RemainingData -= bytesRec;
                            socketReadAttempt--;
                        }

                        try
                        {
                            gameListData = JsonConvert.DeserializeObject<GameListData>(receivedStr);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "[FRS] Failed serialization on server " + socketData.Server);
                            _logger.Error("Serialized Message: " + Environment.NewLine + receivedStr);
                            reconnectSockets = true;
                            if (socketData.CachedGameListData != null)
                            {
                                gameListDataList.Add(socketData.CachedGameListData);
                            }
                            continue;
                        }

                        socketData.CachedGameListData = gameListData;
                        gameListData.Server = socketData.Server;
                        if (gameListData.Lobby.IsAvailable && gameListData.Lobby.PlayerDataList == null)
                        {
                            gameListData.Lobby.PlayerDataList = new List<GameListPlayerData>();
                        }
                        if (gameListData.ProgressList == null)
                        {
                            //Add dummy list otherwise nancyfx SSVE can't render page correctly
                            gameListData.ProgressList = new List<GameListProgressData>();
                        }
                        else
                        {
                            foreach (GameListProgressData gpd in gameListData.ProgressList)
                            {
                                gpd.Server = socketData.Server;
                                SetPlayerFateGameInfo(gpd);
                            }
                        }
                        gameListDataList.Add(gameListData);
                    }
                    else
                    {
                        // Polled failed
                        reconnectSockets = true;
                        _logger.Error("[FRS] Poll failed for server: " + socketData.Server);
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "[FRS] GetGameList Comm Error for server: " + socketData.Server);
                    reconnectSockets = true;
                }
            }

            if (reconnectSockets)
            {
                _logger.Error("Reconnecting FRS sockets after error");
                ConnectAllSockets();
            }
            return gameListDataList;
        }

        public void RefreshBanList()
        {
            bool reconnectSockets = false;
            foreach (SocketData socketData in _socketList)
            {
                if (!IsSocketConnected(socketData.Socket))
                {
                    reconnectSockets = true;
                    break;
                }
            }

            if (reconnectSockets)
            {
                ConnectAllSockets();
            }

            foreach (SocketData socketData in _socketList)
            {
                try
                {
                    byte[] cmd = Encoding.ASCII.GetBytes(REFRESH_BAN_LIST);
                    socketData.Socket.Send(cmd);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "[FRS] RefreshBanList Error on " + socketData.Server);
                }
            }
        }
    }
}
