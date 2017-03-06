using System;
using System.Collections.Generic;
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

        public bool UnbanPlayer(string playerName, string adminName)
        {
            using (var db = frsDatabase.Create())
            {
                ban bannedPlayer =
                    db.ban.FirstOrDefault(
                        x => x.PlayerName.Equals(playerName, StringComparison.InvariantCultureIgnoreCase) &&
                             x.IsCurrentlyBanned);

                if (bannedPlayer == null)
                    return false;

                bannedPlayer.IsCurrentlyBanned = false;
                bannedPlayer.IsPermanentBan = false;
                bannedPlayer.ModifiedByAdmin = adminName;
                bannedPlayer.ModifiedDateTime = DateTime.Now;

                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<PlayerBanData> GetCurrentlyBannedPlayers(bool excludePermanentBans)
        {
            using (var db = frsDatabase.Create())
            {
                IQueryable<ban> query = db.ban;

                if (excludePermanentBans)
                {
                    query = query.Where(x => x.IsCurrentlyBanned && !x.IsPermanentBan);
                }
                else
                {
                    query = query.Where(x => x.IsCurrentlyBanned);
                }

                var playedBanDataList =
                    from bannedPlayers in query.AsEnumerable()
                    select new PlayerBanData
                    {
                        Admin = bannedPlayers.Admin,
                        BannedDateTime = bannedPlayers.BannedDateTime,
                        BannedUntil = bannedPlayers.BannedUntil,
                        IsPermanentBan = bannedPlayers.IsPermanentBan,
                        PlayerName = bannedPlayers.PlayerName,
                        Reason = bannedPlayers.Reason,
                        IpAddresses = bannedPlayers.IpAddresses.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries).ToList()
                    };

                return playedBanDataList.ToList();
            }
        }
    }
}
