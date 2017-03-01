using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Data.GHost;

namespace Fate.DB.DAL.GHost
{
    public class GHostPlayerDataDAL
    {
        public List<GHostPlayerSearchData> SearchByPlayerName(string playerName)
        {
            using (var db = ghostEntities.Create())
            {

                var playerSearchData = from gamePlayers in db.gameplayers
                    join games in db.games on gamePlayers.gameid equals games.id
                    where gamePlayers.name.Equals(playerName, StringComparison.InvariantCultureIgnoreCase)
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
