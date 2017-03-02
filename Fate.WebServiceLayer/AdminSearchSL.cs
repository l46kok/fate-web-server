using System.Collections.Generic;
using Fate.Common.Data.GHost;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    public class AdminSearchSL
    {
        private static readonly GHostPlayerDataDAL ghostPlayerDataDAL = new GHostPlayerDataDAL();
        public static AdminSearchSL Instance { get; } = new AdminSearchSL();

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
