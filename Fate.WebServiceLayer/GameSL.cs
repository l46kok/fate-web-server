using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.DB.DAL;
using Fate.DB.Entity;

namespace Fate.WebServiceLayer
{
    public class GameSL
    {
        public IEnumerable<GameData> GetRecentGames(int count)
        {
            GameDAL gameDal = new GameDAL();
            List<GameData> recentGameList = gameDal.GetRecentGameDataList(count).ToList();
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
            GameDAL gameDal = new GameDAL();
            return gameDal.GetGameLog(gameId);
        }
    }
}
