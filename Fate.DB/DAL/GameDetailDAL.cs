using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.DB.Entity;

namespace Fate.DB.DAL
{
    public class GameDetailDAL
    {
        public List<GamePlayerDetailData> GetGameDetails(int gameId)
        {
            using (frsEntities db = new frsEntities())
            {
                var gameDetailQueryData = (
                    from game in db.GamePlayerDetail
                    join heroType in db.HeroType on game.FK_HeroTypeID equals heroType.HeroTypeID
                    join player in db.Player on game.FK_PlayerID equals player.PlayerID
                    where game.FK_GameID == gameId
                    select new GamePlayerDetailData()
                    {
                        PlayerName = player.PlayerName,
                        HeroUnitTypeID = heroType.HeroUnitTypeID,
                        Kills = game.Kills,
                        Assists = game.Assists,
                        Deaths = game.Deaths,
                        Team = game.Team,
                        GoldSpent = game.GoldSpent
                    });
                return gameDetailQueryData.ToList();
            }
        } 
    }
}
