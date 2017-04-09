using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

        public List<GHostPlayerSearchData> SearchByPlayerName(string playerName, int filterType, string filterRangeInput)
        {
            GHostPlayerDAL.GPSearchType searchType = GetGhostPlayerSearchType(filterType);
            DateTime? filterDateRange = null;
            if (!String.IsNullOrEmpty(filterRangeInput))
            {
                filterDateRange = DateTime.ParseExact(filterRangeInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            return _ghostPlayerDal.SearchByPlayerName(playerName, searchType, filterDateRange);
        }

        public List<GHostPlayerSearchData> SearchByIp(string ip)
        {
            return _ghostPlayerDal.SearchByIp(ip);
        }

        private GHostPlayerDAL.GPSearchType GetGhostPlayerSearchType(int filterType)
        {
            switch (filterType)
            {
                case 1:
                    return GHostPlayerDAL.GPSearchType.All;
                case 2:
                    return GHostPlayerDAL.GPSearchType.Before;
                case 3:
                    return GHostPlayerDAL.GPSearchType.After;
                default:
                    throw new InvalidEnumArgumentException("GPSearchType Invalid Enum");
            }
        }
    }
}
