using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Data.GHost;

namespace Fate.DB.DAL.GHost
{
    public class GHostPlayerDAL
    {
        public enum GPSearchType
        {
            All,
            Before,
            After
        }

        public List<GHostPlayerSearchData> SearchByPlayerName(string playerName, GPSearchType searchType, DateTime? dateTimeRange)
        {
            using (var db = ghostEntities.Create())
            {
                var playerSearchData = from gamePlayers in db.gameplayers
                                       join games in db.games on gamePlayers.gameid equals games.id
                                       where gamePlayers.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase)
                                       select new GHostPlayerSearchData()
                                       {
                                           PlayerName = gamePlayers.name,
                                           DateTime = games.datetime,
                                           Duration = games.duration,
                                           GameName = games.gamename,
                                           Ip = gamePlayers.ip,
                                           LeftReason = gamePlayers.leftreason,
                                           SpoofedRealm = gamePlayers.spoofedrealm
                                       };
                                        
                switch (searchType)
                {
                    case GPSearchType.All:
                        break;
                    case GPSearchType.After:
                        playerSearchData = playerSearchData.Where(x => x.DateTime >= dateTimeRange.Value);
                        break;
                    case GPSearchType.Before:
                        playerSearchData = playerSearchData.Where(x => x.DateTime <= dateTimeRange.Value);
                        break;
                }

                playerSearchData = playerSearchData.OrderByDescending(x => x.DateTime);

                return playerSearchData.Take(50).ToList();
            }
        }

        public List<GHostPlayerSearchData> SearchByIp(string ipAddress)
        {
            using (var db = ghostEntities.Create())
            {

                var playerSearchData = from gamePlayers in db.gameplayers
                                       join games in db.games on gamePlayers.gameid equals games.id
                                       where gamePlayers.ip == ipAddress
                                       orderby games.datetime descending
                                       select new GHostPlayerSearchData()
                                       {
                                           PlayerName = gamePlayers.name,
                                           DateTime = games.datetime,
                                           Duration = games.duration,
                                           GameName = games.gamename,
                                           Ip = gamePlayers.ip,
                                           LeftReason = gamePlayers.leftreason,
                                           SpoofedRealm = gamePlayers.spoofedrealm
                                       };

                return playerSearchData.Take(50).ToList();
            }
        }

        public void UnbanPlayer(string playerName)
        {
            using (var db = ghostEntities.Create())
            {
                bans banData = db.bans.Single(x => x.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase));
                db.bans.Remove(banData);
                db.SaveChanges();
            }
        }

        public void BanPlayer(PlayerBanData playerBanData)
        {
            using (var db = ghostEntities.Create())
            {
                if (playerBanData.IpAddresses.Any())
                {
                    foreach (string ipAddress in playerBanData.IpAddresses)
                    {
                        bans ban = new bans
                        {
                            id = 300,
                            name = playerBanData.PlayerName,
                            admin = playerBanData.Admin,
                            reason = playerBanData.Reason,
                            ip = ipAddress,
                            botid = 1,
                            date = DateTime.Now,
                            server = "",
                            gamename = ""
                        };
                        db.bans.Add(ban);
                    }
                }
                else
                {
                    bans ban = new bans
                    {
                        name = playerBanData.PlayerName,
                        admin = playerBanData.Admin,
                        reason = playerBanData.Reason,
                        botid = 1,
                        date = DateTime.Now,
                        server = "",
                        gamename = "",
                        ip = ""
                    };
                    db.bans.Add(ban);
                }
                db.Database.CommandTimeout = 300;
                db.SaveChanges();
            }
        }
    }
}
