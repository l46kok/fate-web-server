﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Utility;
using Fate.DB.DAL.FRS;

namespace Fate.WebServiceLayer
{
    public class GameSL
    {
        private static readonly GameDAL _gameDal = new GameDAL();
        public static GameSL Instance { get; } = new GameSL();

        private GameSL()
        {
            
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
            string assemblyLocation = PathHandler.GetAssemblyPath();
            if (string.IsNullOrEmpty(assemblyLocation))
                return null;
            data.ReplayPath = Path.Combine(assemblyLocation, ContentURL.GetLocalReplayPath(data.ReplayPath));
            return data;
        }
    }
}
