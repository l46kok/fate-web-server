using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public enum PStatOrderType
        {
            WinRate,
            KDA,
            DamageDealt,
            DamageTaken
        };

        public enum PStatTime
        {
            LastSevenDays,
            LastThirtyDays,
            AllTime
        }

        public List<PlayerStatisticsData> GetPlayerStatistics(PStatOrderType orderType, PStatTime time)
        {
            string orderByClause;
            switch (orderType)
            {
                case PStatOrderType.WinRate:
                    orderByClause = "ORDER BY WinRate DESC";
                    break;
                case PStatOrderType.KDA:
                    orderByClause = "ORDER BY KDA DESC";
                    break;
                case PStatOrderType.DamageDealt:
                    orderByClause = "ORDER BY AvgDamageDealt DESC";
                    break;
                case PStatOrderType.DamageTaken:
                    orderByClause = "ORDER BY AvgDamageTaken DESC";
                    break;
                default:
                    throw new InvalidEnumArgumentException("PStatOrderType Invalid Enum");
            }

            string timeClause;
            switch (time)
            {
                case PStatTime.LastSevenDays:
                    timeClause = "WHERE PlayedDate BETWEEN DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND CURDATE()";
                    break;
                case PStatTime.LastThirtyDays:
                    timeClause = "WHERE PlayedDate BETWEEN DATE_SUB(CURDATE(), INTERVAL 30 DAY) AND CURDATE()";
                    break;
                case PStatTime.AllTime:
                    timeClause = "";
                    break;
                default:
                    throw new InvalidEnumArgumentException("PStatTime Invalid Enum");
            }

            string sql =
                $@"SELECT PlayerName, Wins, Losses, Wins / (Wins + Losses) AS WinRate, (Kills + Assists) / (Deaths) AS KDA, AvgDamageDealt, AvgDamageTaken, AvgGoldSpent
                        FROM (
                        SELECT 
	                        C.PlayerName, 
                            SUM(CASE WHEN B.Result = 'LOSS' THEN 1 ELSE 0 END) Losses, 
                            SUM(CASE WHEN B.Result = 'WIN' THEN 1 ELSE 0 END) Wins,
                            SUM(B.Kills) AS Kills,
                            SUM(B.Deaths) AS Deaths,
                            SUM(B.Assists) AS Assists,
                            SUM(B.DamageDealt) / COUNT(B.DamageDealt) AS AvgDamageDealt,
                            SUM(B.DamageTaken) / COUNT(B.DamageTaken) AS AvgDamageTaken,
                            SUM(B.GoldSpent) / COUNT(B.GoldSpent) AS AvgGoldSpent
                        FROM Game A
                        JOIN GamePlayerDetail B
	                        ON A.GameID = B.FK_GameID
                        JOIN Player C
	                        ON B.FK_PlayerID = C.PlayerID
                        {timeClause}
                        GROUP BY C.PlayerName
                        HAVING COUNT(C.PlayerID) >= 10
                        ) Result
                        {orderByClause}";

            using (var db = frsDb.Create())
            {
                List<PlayerStatisticsData> playerStatistics = db.Database.SqlQuery<PlayerStatisticsData>(sql).ToList();
                return playerStatistics;
            }
        }
    }
}
