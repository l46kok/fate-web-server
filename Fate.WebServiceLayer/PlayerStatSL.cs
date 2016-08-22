using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.DB.DAL;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    public class PlayerStatSL
    {
        private static readonly PlayerStatDAL _playerStatDal = new PlayerStatDAL();
        private static readonly PlayerStatSL _instance = new PlayerStatSL();
        public static PlayerStatSL Instance => _instance;

        private PlayerStatSL() { }

        public PlayerStatsPageViewModel GetPlayerStatSummary(string playerName, string serverName, int lastGameId)
        {
            PlayerStatsPageViewModel vm = new PlayerStatsPageViewModel
            {
                UserName = playerName,
                Server = serverName
            };
            //Get player total summary first
            PlayerStatSummaryData summaryData = _playerStatDal.GetPlayerSummary(playerName, serverName);
            if (summaryData == null)
            {
                vm.HasFoundUser = false;
                return vm;
            }

            vm.UserName = summaryData.PlayerName;
            vm.HasFoundUser = true;
            vm.Wins = summaryData.Win;
            vm.Losses = summaryData.Loss;
            vm.WLPercent = $"{(vm.Wins*1.0/(vm.Wins + vm.Losses)):0.0%}";
            vm.LastPlayedDateTime = summaryData.LastGamePlayed;
            double avgPlayerKills = (summaryData.TotalPlayerKills*1.0/summaryData.TotalPlayerGameCount);
            double avgPlayerDeaths = (summaryData.TotalPlayerDeaths * 1.0 / summaryData.TotalPlayerGameCount);
            double avgPlayerAssists = (summaryData.TotalPlayerAssists * 1.0 / summaryData.TotalPlayerGameCount);
            vm.AveragePlayerKills = avgPlayerKills.ToString("0.0");
            vm.AveragePlayerDeaths = avgPlayerDeaths.ToString("0.0");
            vm.AveragePlayerAssists = avgPlayerAssists.ToString("0.0");
            vm.AveragePlayerKDA =
                ((avgPlayerKills + avgPlayerAssists) / avgPlayerDeaths)
                    .ToString("0.00");

            
            //Get player hero summary 
            List<PlayerHeroStatSummaryData> heroStatSummaryData =
                _playerStatDal.GetPlayerHeroSummary(summaryData.PlayerId);
            List<PlayerHeroStatsViewModel> vmHeroStats = new List<PlayerHeroStatsViewModel>();
            foreach (PlayerHeroStatSummaryData heroSummary in heroStatSummaryData)
            {
                PlayerHeroStatsViewModel vmHero = new PlayerHeroStatsViewModel
                {
                    HeroImageURL = ContentURL.GetHeroIconURL(heroSummary.HeroUnitTypeID),
                    HeroName = heroSummary.HeroName,
                    HeroWins = heroSummary.Wins,
                    HeroLosses = heroSummary.Losses,
                    HeroWLPercent = $"{(heroSummary.Wins*1.0/(heroSummary.Wins + heroSummary.Losses)):0.0%}"
                };
                double avgHeroKills = (heroSummary.HeroTotalKills*1.0 / heroSummary.HeroTotalPlayCount);
                double avgHeroDeaths = (heroSummary.HeroTotalDeaths * 1.0 / heroSummary.HeroTotalPlayCount);
                double avgHeroAssists = (heroSummary.HeroTotalAssists * 1.0 / heroSummary.HeroTotalPlayCount);
                vmHero.HeroAverageKills = avgHeroKills.ToString("0.0");
                vmHero.HeroAverageDeaths = avgHeroDeaths.ToString("0.0");
                vmHero.HeroAverageAssists = avgHeroAssists.ToString("0.0");
                vmHero.HeroAverageKDA = ((avgHeroKills + avgHeroAssists) / avgHeroDeaths).ToString("0.00");
                vmHeroStats.Add(vmHero);

            }
            vm.PlayerHeroStatSummaryData = vmHeroStats;
            vm.PlayerGameSummaryData = GetPlayerGameSummary(summaryData.PlayerId,lastGameId);
            vm.LastGameID = vm.PlayerGameSummaryData.Min(x => x.GameID);
            return vm;
        }

        public List<PlayerGameSummaryViewModel> GetPlayerGameSummary(string playerName, string serverName, int lastGameId)
        {
            //Get player total summary first
            PlayerStatSummaryData summaryData = _playerStatDal.GetPlayerSummary(playerName, serverName);
            if (summaryData == null)
            {
                return null;
            }
            return GetPlayerGameSummary(summaryData.PlayerId, lastGameId);
        }

        private List<PlayerGameSummaryViewModel> GetPlayerGameSummary(int playerId, int lastGameId)
        {
            //Get player game summary
            List<PlayerGameSummaryData> playerGameSummaryData = _playerStatDal.GetPlayerGameSummaryData(playerId, lastGameId);
            List<PlayerGameSummaryViewModel> vmGameSummary = new List<PlayerGameSummaryViewModel>();
            foreach (PlayerGameSummaryData gameSummary in playerGameSummaryData)
            {
                PlayerGameSummaryViewModel vmGame = new PlayerGameSummaryViewModel
                {
                    GameID = gameSummary.GameID,
                    PlayedDate = gameSummary.PlayedDate.ToShortDateString(),
                    GoldSpent = $"{gameSummary.GoldSpent:n0}",
                    GameResult = gameSummary.GameResult,
                    HeroKills = gameSummary.HeroKills,
                    HeroDeaths = gameSummary.HeroDeaths,
                    HeroAssists = gameSummary.HeroAssists,
                    HeroLevel = gameSummary.HeroLevel,
                    DamageDealt = $"{((int)gameSummary.DamageDealt):n0}",
                    DamageTaken = $"{((int)gameSummary.DamageTaken):n0}",
                    HeroKDA =
                        ((gameSummary.HeroKills * 1.0 + gameSummary.HeroAssists) / gameSummary.HeroDeaths).ToString("0.00"),
                    HeroImageURL = ContentURL.GetHeroIconURL(gameSummary.HeroUnitTypeID),
                    TeamOneWinCount = gameSummary.TeamOneWinCount,
                    TeamTwoWinCount = gameSummary.TeamTwoWinCount
                };
                foreach (PlayerGameTeamPlayerData gameTeamPlayerData in gameSummary.TeamList)
                {
                    gameTeamPlayerData.HeroImageURL = ContentURL.GetHeroIconURL(gameTeamPlayerData.HeroUnitTypeID);
                    if (gameTeamPlayerData.Team == "1")
                        vmGame.Team1List.Add(gameTeamPlayerData);
                    else
                        vmGame.Team2List.Add(gameTeamPlayerData);
                }
                vmGameSummary.Add(vmGame);
            }
            return vmGameSummary;
        }
    }
}
