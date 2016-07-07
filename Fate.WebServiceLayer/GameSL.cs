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
            IEnumerable<GameData> recentGameList = gameDal.GetRecentGameDataList(count);
            return recentGameList;
        }
    }
}
