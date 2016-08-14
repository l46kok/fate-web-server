using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using Fate.Common.Data;
using Newtonsoft.Json;

namespace Fate.WebServiceLayer
{
    public class GameListSL
    {
        private const int GHOST_FRS_PORT = 6302;
        private const string GHOST_CONNECT_IP = "127.0.0.1";
        private const string GET_GAMES_COMMAND = "GetGames";
        private static readonly GameListSL _instance = new GameListSL();
        private readonly Socket _socket;

        public static GameListSL Instance => _instance;

        private GameListSL()
        {
            try
            {
                //TO DO: For now, client socket uses blocking appraoch
                //Change it to Async approach later if we experience performance issue
                //Also, we're keeping the socket alive at all times for performance reason 
                IPHostEntry ipHostInfo = Dns.GetHostEntry(GHOST_CONNECT_IP);
                IPAddress ipAddress = null;
                foreach (IPAddress ipAddr in ipHostInfo.AddressList)
                {
                    if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ipAddr;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, GHOST_FRS_PORT);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(remoteEP);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FRS GHost socket connection error\n" + ex);
            }
        }

        public GameListData GetGameList()
        {
            try
            {
                byte[] bytes = new byte[1024];
                byte[] msg = Encoding.ASCII.GetBytes(GET_GAMES_COMMAND);

                _socket.Send(msg);

                int bytesRec = _socket.Receive(bytes);
                string receivedStr = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                return JsonConvert.DeserializeObject<GameListData>(receivedStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetGameList Comm Error: "+ ex);
                return null;
            }
        }
    }
}
