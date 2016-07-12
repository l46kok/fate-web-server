using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.DB.Entity;

namespace Fate.DB.DAL
{
    public class PlayerStatDAL
    {
        public PlayerStatSummaryData GetPlayerSummary(string playerName, string serverName)
        {
            using (frsEntities db = new frsEntities())
            {
                PlayerStatSummaryData summaryData = (
                    from playerStat in db.PlayerStat
                    join player in db.Player on playerStat.FK_PlayerID equals player.PlayerID
                    join server in db.Server on player.FK_ServerID equals server.ServerID
                    join playerHeroStat in db.PlayerHeroStat on player.PlayerID equals playerHeroStat.FK_PlayerID
                    where playerName == player.PlayerName &&
                          serverName == server.ServerName
                    group playerHeroStat by new
                    {
                        player.PlayerID,
                        player.PlayerName,
                        playerStat.Win,
                        playerStat.Loss
                    }
                    into g
                    select new PlayerStatSummaryData()
                    {
                        PlayerId = g.Key.PlayerID,
                        PlayerName = g.Key.PlayerName,
                        Win = g.Key.Win,
                        Loss = g.Key.Loss,
                        TotalPlayerKills = g.Sum(x=>x.TotalHeroKills),
                        TotalPlayerDeaths = g.Sum(x => x.TotalHeroDeaths),
                        TotalPlayerAssists = g.Sum(x => x.TotalHeroAssists),
                        TotalPlayerGameCount = g.Sum(x=> x.HeroPlayCount)
                    }).FirstOrDefault();

                if (summaryData == null)
                    return null;

                DateTime lastGamePlayed = (
                    from game in db.Game
                    join gameDetail in db.GamePlayerDetail on game.GameID equals gameDetail.FK_GameID
                    where gameDetail.FK_PlayerID == summaryData.PlayerId
                    orderby game.PlayedDate descending
                    select game.PlayedDate
                    ).First();

                summaryData.LastGamePlayed = lastGamePlayed;
                return summaryData;
            }
        }

        public List<PlayerHeroStatSummaryData> GetPlayerHeroSummary(int playerId)
        {
            using (frsEntities db = new frsEntities())
            {
                var heroSummaryQuery = (
                    from gameDetail in db.GamePlayerDetail
                    join heroType in db.HeroType on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
                    where gameDetail.FK_PlayerID == playerId
                    group gameDetail by new
                    {
                        gameDetail.Result,
                        heroType.HeroUnitTypeID
                    }
                    into g
                    select new
                    {
                        g.Key.HeroUnitTypeID,
                        g.Key.Result,
                        ResultCount = g.Count()
                    });
                List<PlayerHeroStatSummaryData> heroStatSummaryList = new List<PlayerHeroStatSummaryData>();
                foreach (var summaryData in heroSummaryQuery)
                {
                    PlayerHeroStatSummaryData heroStatSummary =
                        heroStatSummaryList.FirstOrDefault(x => x.HeroUnitTypeID == summaryData.HeroUnitTypeID);
                    if (heroStatSummary == null)
                    {
                        heroStatSummary = new PlayerHeroStatSummaryData {HeroUnitTypeID = summaryData.HeroUnitTypeID};
                        heroStatSummaryList.Add(heroStatSummary);
                    }
                    if (summaryData.Result == "WIN")
                    {
                        heroStatSummary.Wins = summaryData.ResultCount;
                    }
                    else if (summaryData.Result == "LOSS")
                    {
                        heroStatSummary.Losses = summaryData.ResultCount;
                    }
                }
                heroStatSummaryList = heroStatSummaryList.OrderByDescending(x => x.HeroTotalPlayCount).ToList();

                var heroSummaryKDAQuery = (
                    from playerHeroStat in db.PlayerHeroStat
                    join heroType in db.HeroType on playerHeroStat.FK_HeroTypeID equals heroType.HeroTypeID
                    join heroName in db.HeroTypeName on heroType.HeroTypeID equals heroName.FK_HeroTypeID
                    where playerHeroStat.FK_PlayerID == playerId
                    select new
                    {
                        heroName.HeroName,
                        heroType.HeroUnitTypeID,
                        playerHeroStat.HeroPlayCount,
                        playerHeroStat.TotalHeroKills,
                        playerHeroStat.TotalHeroDeaths,
                        playerHeroStat.TotalHeroAssists
                    });

                foreach (var kdaData in heroSummaryKDAQuery)
                {
                    PlayerHeroStatSummaryData heroStatSummary =
                        heroStatSummaryList.First(x => x.HeroUnitTypeID == kdaData.HeroUnitTypeID);
                    heroStatSummary.HeroTotalPlayCount = kdaData.HeroPlayCount;
                    heroStatSummary.HeroTotalKills = kdaData.TotalHeroKills;
                    heroStatSummary.HeroTotalDeaths = kdaData.TotalHeroDeaths;
                    heroStatSummary.HeroTotalAssists = kdaData.TotalHeroAssists;
                    heroStatSummary.HeroName = kdaData.HeroName;
                }
                return heroStatSummaryList;
            }
        }

        public List<PlayerGameSummaryData> GetPlayerGameSummaryData(int playerId)
        {
            using (frsEntities db = new frsEntities())
            {
                List<PlayerGameSummaryData> gameSummaryData = (
                    from game in db.Game
                    join gameDetail in db.GamePlayerDetail on game.GameID equals gameDetail.FK_GameID
                    join heroType in db.HeroType on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
                    where gameDetail.FK_PlayerID == playerId
                    orderby game.GameID descending
                    select new PlayerGameSummaryData()
                    {
                        GameID = game.GameID,
                        GoldSpent = gameDetail.GoldSpent,
                        PlayedDate = game.PlayedDate,
                        GameResult = gameDetail.Result,
                        HeroKills = gameDetail.Kills,
                        HeroDeaths = gameDetail.Deaths,
                        HeroAssists = gameDetail.Assists,
                        HeroUnitTypeID = heroType.HeroUnitTypeID,
                        TeamOneWinCount = game.TeamOneWinCount,
                        TeamTwoWinCount = game.TeamTwoWinCount
                    }).ToList();

                foreach (PlayerGameSummaryData data in gameSummaryData)
                {
                    var teamQuery = (
                        from gameDetail in db.GamePlayerDetail
                        join player in db.Player on gameDetail.FK_PlayerID equals player.PlayerID
                        join heroType in db.HeroType on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
                        where gameDetail.FK_GameID == data.GameID
                        select new
                        {
                            player.PlayerName,
                            heroType.HeroUnitTypeID,
                            gameDetail.Team
                        }
                        );
                    foreach (var teamData in teamQuery)
                    {
                        PlayerGameTeamPlayerData teamPlayer = new PlayerGameTeamPlayerData
                        {
                            PlayerName = teamData.PlayerName,
                            HeroUnitTypeID = teamData.HeroUnitTypeID,
                            Team = teamData.Team
                        };
                        data.TeamList.Add(teamPlayer);
                    }
                }

                return gameSummaryData;
            }
        }
    }
}
