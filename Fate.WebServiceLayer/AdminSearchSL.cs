using System.Collections.Generic;
using Fate.Common.Data.GHost;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    public class AdminSearchSL
    {
        private static readonly GHostPlayerDAL _ghostPlayerDal = new GHostPlayerDAL();
        public static AdminSearchSL Instance { get; } = new AdminSearchSL();

        public List<GHostPlayerSearchData> SearchByPlayerName(string playerName)
        {
            return _ghostPlayerDal.SearchByPlayerName(playerName);
        }

        public List<GHostPlayerSearchData> SearchByIp(string ip)
        {
            return _ghostPlayerDal.SearchByIp(ip);
        }
    }
}
