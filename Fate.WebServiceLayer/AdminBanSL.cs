using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Fate.Common.Data;
using Fate.Common.Data.GHost;
using Fate.DB;
using Fate.DB.DAL.FRS;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    public class AdminBanSL
    {
        private static readonly BanDAL _adminBanDal = new BanDAL();
        private static readonly GHostPlayerDataDAL _ghostPlayerDataDal = new GHostPlayerDataDAL();
        private static readonly AdminBanSL _instance = new AdminBanSL();
        private static readonly GameListSL _gameListSl = GameListSL.Instance;
        public static AdminBanSL Instance => _instance;

        public bool BanPlayer(PlayerBanData playerBanData, List<GHostDatabaseInfo> ghostDatabaseList)
        {
            if (_adminBanDal.IsPlayerBanned(playerBanData.PlayerName))
            {
                return false;
            }

            _adminBanDal.BanPlayer(playerBanData);
            foreach (var ghostDbInfo in ghostDatabaseList)
            {
                ghostEntities.InitDatabaseConnection(ghostDbInfo);
                _ghostPlayerDataDal.BanPlayer(playerBanData);
            }

            _gameListSl.RefreshBanList();


            return true;
        }

        public string ValidateBanForm(PlayerBanData banData)
        {
            if (String.IsNullOrEmpty(banData.PlayerName))
            {
                return "Player name cannot be empty";
            }

            if (banData.PlayerName.Equals("l46kok", StringComparison.InvariantCultureIgnoreCase) ||
                banData.IpAddresses.Any(x => x.StartsWith("104.181.20")))
            {
                return "You cannot ban that is holy and sacred.";
            }

            if (banData.IpAddresses.Contains("127.0.0.1"))
            {
                return "127.0.0.1 is not a bannable IP.";
            }
            if (banData.BannedUntil != null && banData.BannedUntil.Value < DateTime.Now)
            {
                return "You cannot input negative numbers for duration";
            }
            if (String.IsNullOrEmpty(banData.Reason))
            {
                return "Ban reason cannot be empty";
            }

            return String.Empty;
        }
    }
}
