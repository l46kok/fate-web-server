using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fate.Common.Data.GHost;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AdminSearchSL
    {
        private readonly GHostPlayerDAL _ghostPlayerDal;

        public AdminSearchSL(GHostPlayerDAL ghostPlayerDal)
        {
            _ghostPlayerDal = ghostPlayerDal;
        }

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
