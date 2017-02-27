using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.Common.Data.GHost;

namespace Fate.DB.DAL.GHost
{
    public class GHostPlayerDataDAL
    {
        public List<GHostPlayerSearchData> GetPlayerSearchData(string playerName)
        {
            using (var db = ghostEntities.Create())
            {

                var playerSearchData = (
                    from gamePlayers in db.gameplayers
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
                    });

                return playerSearchData.ToList();
            }
        }
    }
}
