using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.DB.Entity;

namespace Fate.DB.DAL
{
    /// <summary>
    ///     Data access layer for Game table
    /// </summary>
    public class GameDAL
    {
        /// <summary>
        /// Retrieves the recently played games on bot
        /// </summary>
        /// <param name="count">Number of recent games to retrieve</param>
        /// <returns>List of GameData</returns>
        public IEnumerable<GameData> GetRecentGameDataList(int count)
        {
            var gameDataList = new List<GameData>();
            using (var db = new frsEntities())
            {
                var recentGameQueryData = (from game in db.Game
                    join server in db.Server on game.FK_ServerID equals server.ServerID
                    orderby game.GameID
                    select new
                    {
                        game.GameName,
                        game.Duration,
                        game.Log,
                        game.MapVersion,
                        game.MatchType,
                        game.PlayedDate,
                        game.ReplayUrl,
                        game.Result,
                        server.ServerName
                    }).Take(count);

                foreach (var data in recentGameQueryData)
                {
                    var gameData = new GameData
                    {
                        GameName = data.GameName,
                        Duration = data.Duration,
                        Log = data.Log,
                        MapVersion = data.MapVersion,
                        MatchType = data.MatchType,
                        PlayedDate = data.PlayedDate,
                        ReplayUrl = data.ReplayUrl,
                        Result = data.Result,
                        ServerName = data.ServerName
                    };
                    gameDataList.Add(gameData);
                }
            }

            return gameDataList;
        }
    }
}