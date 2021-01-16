using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Utility;
using Fate.DB.DAL.FRS;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class GameSL
    {
        private readonly GameDAL _gameDal;

        public GameSL(GameDAL gameDal)
        {
            _gameDal = gameDal;
        }

        public IEnumerable<GameData> GetRecentGames(int count)
        {
            
            List<GameData> recentGameList = _gameDal.GetRecentGameDataList(count).ToList();
            foreach (GameData data in recentGameList)
            {
                if (data.TeamOneWinCount > data.TeamTwoWinCount)
                {
                    data.Result = $@"Team 1 Victory ({data.TeamOneWinCount}-{data.TeamTwoWinCount})";
                }
                else if (data.TeamOneWinCount < data.TeamTwoWinCount)
                {
                    data.Result = $@"Team 2 Victory ({data.TeamOneWinCount}-{data.TeamTwoWinCount})";
                }
                else
                {
                    data.Result = $@"Draw ({data.TeamOneWinCount}-{data.TeamTwoWinCount})";
                }
            }
            return recentGameList;
        }

        public string GetGameLog(int gameId)
        {
            return _gameDal.GetGameLog(gameId);
        }

        public GameReplayData GetReplayData(int gameId)
        {
            GameReplayData data = _gameDal.GetReplayData(gameId);
            if (data == null)
                return null;
            if (string.IsNullOrEmpty(data.ReplayPath))
                return null;
            data.ReplayPath = Path.Combine(ConfigHandler.ReplayFileLocation, Path.GetFileName(data.ReplayPath));
            return data;
        }
    }
}
