using System.Collections.Generic;
using Fate.Common.Data.GHost;
using Fate.DB;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    public class AdminSearchPlayerDataSL
    {
        private static readonly GHostPlayerDataDAL ghostPlayerDataDAL = new GHostPlayerDataDAL();
        private static readonly AdminSearchPlayerDataSL _instance = new AdminSearchPlayerDataSL();
        public static AdminSearchPlayerDataSL Instance => _instance;

        public List<GHostPlayerSearchData> GetPlayerSearchData(string playerName)
        {
            return ghostPlayerDataDAL.GetPlayerSearchData(playerName);
        }
    }
}
