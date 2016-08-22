using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Fate.Common.Data;
using Newtonsoft.Json;

namespace Fate.WebServiceLayer
{
    public class GameListSL
    {
        private const int GHOST_FRS_PORT = 6302;
        private const string GHOST_CONNECT_IP = "54.210.38.182";
        private const string GHOST_CONNECT_IP_EU = "52.210.121.172";
        private const string GET_GAMES_COMMAND = "GetGames";
        private static readonly GameListSL _instance = new GameListSL();
        private readonly List<SocketData> _socketList = new List<SocketData>();

        public static GameListSL Instance => _instance;

        private class SocketData
        {
            public Socket Socket { get; set; }
            public string Server { get; set; }
        }

        private GameListSL()
        {
            ConnectSocket(GHOST_CONNECT_IP, GHOST_FRS_PORT, "USEast");
            ConnectSocket(GHOST_CONNECT_IP_EU, GHOST_FRS_PORT, "Europe");
        }

        private void ConnectSocket(string remoteIpAddr, int port, string server)
        {
            try
            {
                //TO DO: For now, client socket uses blocking appraoch
                //Change it to Async approach later if we experience performance issue
                //Also, we're keeping the socket alive at all times for performance reason 
                IPHostEntry ipHostInfo = Dns.GetHostEntry(remoteIpAddr);
                IPAddress ipAddress = null;
                foreach (IPAddress ipAddr in ipHostInfo.AddressList)
                {
                    if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ipAddr;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                SocketData socketData = new SocketData();
                socketData.Socket = socket;
                socketData.Server = server;
                _socketList.Add(socketData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FRS GHost socket connection error\n" + ex);
            }
        }

        public List<GameListData> GetGameList()
        {
            List<GameListData> gameListDataList = new List<GameListData>();
            foreach (SocketData socketData in _socketList)
            {
                try
                {
                    byte[] bytes = new byte[1024];
                    byte[] msg = Encoding.ASCII.GetBytes(GET_GAMES_COMMAND);

                    socketData.Socket.Send(msg);

                    int bytesRec = socketData.Socket.Receive(bytes);
                    string receivedStr = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    GameListData gameListData = JsonConvert.DeserializeObject<GameListData>(receivedStr);
                    gameListData.Server = socketData.Server;
                    if (gameListData.ProgressList == null)
                    {
                        //Add dummy list otherwise nancyfx SSVE can't render page correctly
                        gameListData.ProgressList = new List<GameProgressData>();
                    }
                    else
                    {
                        //Likewise, SSVE doesn't have a way to nest iterators or get parent's property, so we set the server again here
                        gameListData.ProgressList.ForEach(x=>x.Server = socketData.Server);
                    }
                    gameListDataList.Add(gameListData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GetGameList Comm Error: " + ex);
                    return null;
                }
            }
            return gameListDataList;
        }
    }
}
