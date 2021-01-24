using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using Fate.Common.Data;

namespace Fate.DB.DAL.FRS
{
    public class StatisticsDAL
    {
        public Dictionary<string, int> GetTotalGamesPlayedVersion()
        {
            var gamePlayCountDict = new Dictionary<string, int>();
            using (frsDatabase db = frsDatabase.Create())
            {
                var gamesPlayedByVersion = (
                    from game in db.game
                    group game by new { game.MapVersion } into g
                    select new
                    {
                        g.Key.MapVersion,
                        PlayedTotal = g.Count()
                    });

                foreach (var version in gamesPlayedByVersion)
                {
                    gamePlayCountDict[version.MapVersion] = version.PlayedTotal;
                }
            }
            return gamePlayCountDict;
        }

        public List<ServantDetailStatData> GetServantDetailStatPoints(int typeId = 0)
        {
            const string sql = @"select 
		                            g.MapVersion,
                                    stf.HeroStatAbilId as StatTypeId,
		                            stf.HeroStatName as StatTypeName,
		                            sum(stl.LearnCount) as Points
                                from HeroTypeName h
                                inner join GamePlayerDetail gpd on (h.FK_HeroTypeId = gpd.FK_HeroTypeId)
                                inner join Game g on (gpd.FK_GameID = g.GameId)
                                inner join HeroStatLearn stl on (gpd.GamePlayerDetailID = stl.FK_GamePlayerDetailID)
                                inner join HeroStatInfo stf on (stl.FK_HeroStatInfoID = stf.HeroStatInfoID)
                                WHERE h.FK_HeroTypeId = @TypeId
                                group by g.MapVersion, stf.HeroStatName;";
            using (var db = frsDatabase.Create())
            {
                List<ServantDetailStatData> servantStatPointDetails = db.Database.SqlQuery<ServantDetailStatData>(sql, new MySqlParameter("TypeId", typeId)).ToList();
                return servantStatPointDetails;
            }
        }

        public List<ServantDetailData> GetServantDetailStatistics(int typeId = 0)
        {
            const string sql = @"select 
                                ht.HeroUnitTypeID as HeroUnitTypeID,
	                            h.HeroTitle as HeroTitle,
	                            g.MapVersion,
	                            SUM(gpd.DamageDealt) as DamageDealt,
                                SUM(gpd.DamageTaken) as DamageTaken,
                                SUM(gpd.GoldSpent) as GoldSpent,
                                SUM(gpd.Kills) as KillCount,
                                SUM(gpd.Assists) as AssistCount,
                                SUM(gpd.Deaths) as DeathCount,
	                            COUNT(*) as PlayCount,
	                            SUM(IF(gpd.Result = 'Win', 1, 0)) as WinCount,
	                            SUM(TIME_TO_SEC(g.Duration)) as GameDuration,
                                SUM(IF(gpd.result = 'WIN', IF(gpd.team = '1', g.TeamOneWinCount - g.TeamTwoWinCount, g.TeamTwoWinCount - g.TeamOneWinCount), 0)) as WinScoreDifference,
                                SUM(IF(gpd.result = 'LOSS', IF(gpd.team = '1', g.TeamTwoWinCount - g.TeamOneWinCount, g.TeamOneWinCount - g.TeamTwoWinCount), 0)) as LossScoreDifference
                            FROM HeroTypeName h
                            INNER JOIN HeroType ht on (h.FK_HeroTypeId = ht.HeroTypeId)
                            INNER JOIN gameplayerdetail gpd on (h.FK_HeroTypeId = gpd.FK_HeroTypeId)
                            INNER JOIN game g on (gpd.FK_GameID = g.GameId)
                            WHERE h.FK_HeroTypeId = @TypeId
                            GROUP BY h.herotitle, g.MapVersion;";
            using (var db = frsDatabase.Create())
            {
                List<ServantDetailData> servantDetails = db.Database.SqlQuery<ServantDetailData>(sql, new MySqlParameter("TypeId", typeId)).ToList();
                return servantDetails;
            }
        }

        public List<ServantStatisticsData> GetServantStatistics()
        {
            const string sql = @"SELECT B.HeroUnitTypeID,
                                B.HeroTypeId,
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
            using (var db = frsDatabase.Create())
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
                    timeClause = "WHERE DATE(PlayedDate) BETWEEN DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND CURDATE()";
                    break;
                case PStatTime.LastThirtyDays:
                    timeClause = "WHERE DATE(PlayedDate) BETWEEN DATE_SUB(CURDATE(), INTERVAL 30 DAY) AND CURDATE()";
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
                        HAVING COUNT(C.PlayerID) >= 5
                        ) Result
                        {orderByClause}";

            using (var db = frsDatabase.Create())
            {
                List<PlayerStatisticsData> playerStatistics = db.Database.SqlQuery<PlayerStatisticsData>(sql).ToList();
                return playerStatistics;
            }
        }
    }
}
