using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class PlayerStatDAL
    {
        public PlayerStatSummaryData GetPlayerSummary(string playerName, string serverName)
        {
            using (frsDb db = frsDb.Create())
            {
                PlayerStatSummaryData summaryData = (
                    from playerStat in db.playerstat
                    join player in db.player on playerStat.FK_PlayerID equals player.PlayerID
                    join server in db.server on player.FK_ServerID equals server.ServerID
                    join playerHeroStat in db.playerherostat on player.PlayerID equals playerHeroStat.FK_PlayerID
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
                    from game in db.game
                    join gameDetail in db.gameplayerdetail on game.GameID equals gameDetail.FK_GameID
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
            using (frsDb db = frsDb.Create())
            {
                var heroSummaryQuery = (
                    from gameDetail in db.gameplayerdetail
                    join heroType in db.herotype on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
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

                var heroSummaryKDAQuery = (
                    from playerHeroStat in db.playerherostat
                    join heroType in db.herotype on playerHeroStat.FK_HeroTypeID equals heroType.HeroTypeID
                    join heroName in db.herotypename on heroType.HeroTypeID equals heroName.FK_HeroTypeID
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

                heroStatSummaryList = heroStatSummaryList.OrderByDescending(x => x.HeroTotalPlayCount).ToList();
                return heroStatSummaryList;
            }
        }

        public List<PlayerGameSummaryData> GetPlayerGameSummaryData(int playerId, int gameId, string heroUnitTypeId)
        {
            using (frsDb db = frsDb.Create())
            {
                List<PlayerGameSummaryData> gameSummaryData = (
                    from game in db.game
                    join gameDetail in db.gameplayerdetail on game.GameID equals gameDetail.FK_GameID
                    join heroType in db.herotype on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
                    where gameDetail.FK_PlayerID == playerId && game.GameID < gameId && heroType.HeroUnitTypeID.Contains(heroUnitTypeId)
                    orderby game.GameID descending
                    select new PlayerGameSummaryData
                    {
                        GameID = game.GameID,
                        GoldSpent = gameDetail.GoldSpent,
                        PlayedDate = game.PlayedDate,
                        GameResult = gameDetail.Result,
                        HeroKills = gameDetail.Kills,
                        HeroDeaths = gameDetail.Deaths,
                        HeroAssists = gameDetail.Assists,
                        HeroUnitTypeID = heroType.HeroUnitTypeID,
                        HeroLevel = gameDetail.HeroLevel,
                        Team = gameDetail.Team,
                        TeamOneWinCount = game.TeamOneWinCount,
                        TeamTwoWinCount = game.TeamTwoWinCount,
                        DamageDealt = gameDetail.DamageDealt,
                        DamageTaken = gameDetail.DamageTaken
                    }).Take(8).ToList();

                foreach (PlayerGameSummaryData data in gameSummaryData)
                {
                    var teamQuery = (
                        from gameDetail in db.gameplayerdetail
                        join player in db.player on gameDetail.FK_PlayerID equals player.PlayerID
                        join heroType in db.herotype on gameDetail.FK_HeroTypeID equals heroType.HeroTypeID
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
