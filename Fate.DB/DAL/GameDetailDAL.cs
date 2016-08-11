using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class GameDetailDAL
    {
        public List<GamePlayerDetailData> GetGameDetails(int gameId)
        {
            using (frsDb db = new frsDb())
            {
                var gameDetailQueryData = (
                    from game in db.gameplayerdetail
                    join heroType in db.herotype on game.FK_HeroTypeID equals heroType.HeroTypeID
                    join player in db.player on game.FK_PlayerID equals player.PlayerID
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
