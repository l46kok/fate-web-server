using System;
using System.Linq;
using Nancy;

namespace Executable.Controllers
{
    public class PlayerSearchModule : NancyModule
    {
        public PlayerSearchModule()
        {
            /*// Get player list by name
            Get["/player/{server}/{name}"] = param =>
            {
                string serverName = param.server;
                string playerName = param.name;

                using (var db = new RankingSystemEntities())
                {
                    var playerData =
                        from player in db.Player
                        from server in db.Server
                        from playerStat in db.PlayerStat
                        where player.PlayerName.StartsWith(playerName)
                                && server.ServerName == serverName
                                && player.FK_ServerID == server.ServerID
                                && playerStat.FK_PlayerID == player.PlayerID
                                && playerStat.FK_ServerID == player.FK_ServerID
                        select
                            new
                            {
                                player.PlayerName,
                                playerStat.Win,
                                playerStat.Loss,
                                playerStat.PlayCount,
                                playerStat.ELO,
                                player.RegDate
                            };

                    return Response.AsJson(playerData.ToArray());
                }
            };

            // Get specific player id
            Get["/player/{id}"] = param =>
            {
                using (var db = new RankingSystemEntities())
                {
                    if (param.id == null)
                        return null;

                    int playerId = 0;
                    if (!int.TryParse(param.id, out playerId))
                        return null;

                    var playerData =
                        from player in db.Player
                        from server in db.Server
                        from playerStat in db.PlayerStat
                        where player.PlayerID == playerId
                                && server.ServerID == player.FK_ServerID
                                && playerStat.FK_PlayerID == player.PlayerID
                                && playerStat.FK_ServerID == player.FK_ServerID
                        select
                            new
                            {
                                player.PlayerName,
                                server.ServerName,
                                playerStat.Win,
                                playerStat.Loss,
                                player.RegDate,
                                playerStat.PlayCount
                            };

                    return Response.AsJson(playerData.ToArray());
                }
            };*/
        }
    }
}
