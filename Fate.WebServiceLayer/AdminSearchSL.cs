using System.Collections.Generic;
using Fate.Common.Data.GHost;
using Fate.DB;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    public class AdminSearchSL
    {
        private static readonly GHostPlayerDataDAL ghostPlayerDataDAL = new GHostPlayerDataDAL();
        private static readonly AdminSearchSL _instance = new AdminSearchSL();
        public static AdminSearchSL Instance => _instance;

        public List<GHostPlayerSearchData> SearchByPlayerName(string playerName)
        {
            return ghostPlayerDataDAL.SearchByPlayerName(playerName);
        }

        public List<GHostPlayerSearchData> SearchByIp(string ip)
        {
            return ghostPlayerDataDAL.SearchByIp(ip);
        }
    }
}
