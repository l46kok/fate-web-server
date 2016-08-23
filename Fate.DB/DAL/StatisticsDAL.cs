using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class StatisticsDAL
    {
        public List<ServantStatisticsData> GetServantStatistics()
        {
            const string sql = @"SELECT B.HeroUnitTypeID,
	                            C.HeroName,
                                C.HeroTitle,
	                            SUM(CASE WHEN Result='WIN' THEN 1 ELSE 0 END) WinCount, 
                                SUM(CASE WHEN Result='LOSS' THEN 1 ELSE 0 END) LossCount, 
                                AVG(Kills) AS AvgKills, 
                                AVG(Deaths) AS AvgDeaths, 
                                AVG(Assists) AS AvgAssists,
                                AVG(GoldSpent) AS AvgGoldSpent, 
                                AVG(DamageDealt) AS AvgDamageDealt, 
                                AVG(DamageTaken) AS AvgDamageTaken
                        FROM GAMEPLAYERDETAIL A
                        JOIN HEROTYPE B
	                        ON A.FK_HeroTypeID = B.HeroTypeID
                        JOIN HEROTYPENAME C
	                        ON C.FK_HeroTypeID = B.HeroTypeID
                        GROUP BY A.FK_HeroTypeID;";
            using (var db = frsDb.Create())
            {
                List<ServantStatisticsData> servantStatistics = db.Database.SqlQuery<ServantStatisticsData>(sql).ToList();
                return servantStatistics;
            }
        }
    }
}
