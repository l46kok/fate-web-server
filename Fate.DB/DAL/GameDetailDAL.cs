using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class GameDetailDAL
    {
        public List<GamePlayerDetailData> GetGameDetails(int gameId)
        {
            using (frsDb db = new frsDb())
            {
                //EF Produces monsterous query on this
                //Perhaps in the future, we need to conver it into raw SQL
                var gameDetailQueryData = (
                    from game in db.gameplayerdetail
                    join heroType in db.herotype on game.FK_HeroTypeID equals heroType.HeroTypeID
                    join player in db.player on game.FK_PlayerID equals player.PlayerID
                    join godshelpuse in db.godshelpuse on game.GamePlayerDetailID equals godshelpuse.FK_GamePlayerDetailID into a
                    from ghUseL in a.DefaultIfEmpty() 
                    join godshelpinfo in db.godshelpinfo on ghUseL.FK_GodsHelpInfoID equals godshelpinfo.GodsHelpInfoID into b
                    from ghInfoL in b.DefaultIfEmpty() //Left outer join in EF... seriously?
                    where (game.FK_GameID == gameId)
                    group new { game, heroType, player, ghUseL, ghInfoL} 
                    by new
                    {
                        game.GamePlayerDetailID,
                        game.Kills,
                        game.Assists,
                        game.Deaths,
                        game.Team,
                        game.GoldSpent,
                        game.DamageDealt,
                        game.DamageTaken,
                        game.HeroLevel,
                        player.PlayerName,
                        heroType.HeroUnitTypeID,
                    } into g
                    select new GamePlayerDetailData()
                    {
                        PlayerName = g.Key.PlayerName,
                        HeroUnitTypeID = g.Key.HeroUnitTypeID,
                        Kills = g.Key.Kills,
                        Assists = g.Key.Assists,
                        Deaths = g.Key.Deaths,
                        Team = g.Key.Team,
                        Level = g.Key.HeroLevel,
                        GoldSpent = g.Key.GoldSpent,
                        DamageDealt = g.Key.DamageDealt,
                        DamageTaken = g.Key.DamageTaken,
                        GodsHelpAbilIDList = g.Select(x=>x.ghInfoL.GodsHelpAbilID).ToList()
                    });
                return gameDetailQueryData.ToList();
            }
        } 
    }
}
