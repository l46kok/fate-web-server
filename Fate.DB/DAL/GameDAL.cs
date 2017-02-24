using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;

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
            using (var db = frsDatabase.Create())
            {
                var recentGameQueryData = (
                    from game in db.game
                    join server in db.server on game.FK_ServerID equals server.ServerID
                    join gameDetail in db.gameplayerdetail on game.GameID equals gameDetail.FK_GameID
                    group game by new { game.GameID,game.GameName, game.PlayedDate,
                                        game.Duration, game.Result, game.MapVersion,
                                        game.ReplayUrl, game.MatchType, server.ServerName,
                                        game.TeamOneWinCount, game.TeamTwoWinCount} into g
                    orderby g.Key.GameID descending
                    select new
                    {
                        g.Key.GameID,
                        g.Key.GameName,
                        g.Key.Duration,
                        g.Key.MapVersion,
                        g.Key.MatchType,
                        g.Key.PlayedDate,
                        g.Key.ReplayUrl,
                        g.Key.Result,
                        g.Key.ServerName,
                        g.Key.TeamOneWinCount,
                        g.Key.TeamTwoWinCount,
                        PlayerCount = g.Count()
                    }).Take(count);

                foreach (var data in recentGameQueryData)
                {
                    var gameData = new GameData
                    {
                        GameID = data.GameID,
                        GameName = data.GameName,
                        Duration = data.Duration,
                        MapVersion = data.MapVersion,
                        MatchType = data.MatchType,
                        PlayedDate = data.PlayedDate,
                        ReplayUrl = data.ReplayUrl,
                        Result = data.Result,
                        ServerName = data.ServerName,
                        PlayerCount = data.PlayerCount,
                        TeamOneWinCount = data.TeamOneWinCount,
                        TeamTwoWinCount = data.TeamTwoWinCount
                    };
                    gameDataList.Add(gameData);
                }
            }

            return gameDataList;
        }

        /// <summary>
        /// Retrieves the chat log from the game
        /// </summary>
        /// <param name="gameId">Game ID of the game</param>
        /// <returns>Log (string)</returns>
        public string GetGameLog(int gameId)
        {
            using (var db = frsDatabase.Create())
            {
                return db.game.FirstOrDefault(x => x.GameID == gameId)?.Log;
            }
        }

        public GameReplayData GetReplayData(int gameId)
        {
            using (var db = frsDatabase.Create())
            {
                GameReplayData data = new GameReplayData();
                game game = db.game.FirstOrDefault(x => x.GameID == gameId);
                if (game == null)
                    return null;
                data.PlayedDateTime = game.PlayedDate;
                data.ReplayPath = game.ReplayUrl;
                return data;
            }
        }
    }
}