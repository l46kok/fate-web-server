using System;
using System.Linq;
using Fate.Common.Data;

namespace Fate.DB.DAL.FRS
{
    public class BanDAL
    {
        public void BanPlayer(PlayerBanData playerBanData)
        {
            using (var db = frsDatabase.Create())
            {
                ban ban = new ban
                {
                    BannedDateTime = DateTime.Now,
                    PlayerName = playerBanData.PlayerName,
                    Admin = playerBanData.Admin,
                    Reason = playerBanData.Reason,
                    IsPermanentBan = playerBanData.IsPermanentBan,
                    BannedUntil = playerBanData.BannedUntil,
                    IsCurrentlyBanned = true,
                    IpAddresses = String.Join(",",playerBanData.IpAddresses)
                };
                db.ban.Add(ban);
                db.SaveChanges();
            }
        }

        public bool IsPlayerBanned(string playerName)
        {
            using (var db = frsDatabase.Create())
            {
                return db.ban.Any(
                    x =>
                        x.PlayerName.Equals(playerName, StringComparison.InvariantCultureIgnoreCase) &&
                        x.IsCurrentlyBanned);
            }
        }
    }
}
