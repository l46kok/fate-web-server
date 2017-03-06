using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Data.GHost;
using Fate.DB;
using Fate.DB.DAL.FRS;
using Fate.DB.DAL.GHost;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AdminBanSL
    {
        // GHostCommSL is injected by Autofac
        public GhostCommSL GHostCommSl { get; set; }

        private readonly BanDAL _banDal = new BanDAL();
        private readonly GHostPlayerDAL _ghostPlayerDal = new GHostPlayerDAL();

        public AdminBanSL(BanDAL banDal, GHostPlayerDAL ghostPlayerDal)
        {
            _banDal = banDal;
            _ghostPlayerDal = ghostPlayerDal;
        }

        public bool BanPlayer(PlayerBanData playerBanData, List<GHostDatabaseInfo> ghostDatabaseList)
        {
            if (_banDal.IsPlayerBanned(playerBanData.PlayerName))
            {
                return false;
            }

            _banDal.BanPlayer(playerBanData);
            foreach (var ghostDbInfo in ghostDatabaseList)
            {
                ghostEntities.InitDatabaseConnection(ghostDbInfo);
                _ghostPlayerDal.BanPlayer(playerBanData);
            }

            GHostCommSl.RefreshBanList();


            return true;
        }

        public bool UnbanPlayer(string playerName, string adminName, List<GHostDatabaseInfo> ghostDatabaseList)
        {
            _banDal.UnbanPlayer(playerName, adminName);
            foreach (var ghostDbInfo in ghostDatabaseList)
            {
                ghostEntities.InitDatabaseConnection(ghostDbInfo);
                _ghostPlayerDal.UnbanPlayer(playerName);
            }
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
