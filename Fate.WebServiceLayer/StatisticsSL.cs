using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.DB.DAL.FRS;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    public class StatisticsSL
    {
        private static readonly StatisticsDAL _statisticsDal = new StatisticsDAL();

        public static StatisticsSL Instance { get; } = new StatisticsSL();

        private StatisticsSL() { }

        public List<ServantStatisticsViewModel> GetServantStatistics()
        {
            List<ServantStatisticsData> statisticsData = _statisticsDal.GetServantStatistics();
            List<ServantStatisticsViewModel> vm = new List<ServantStatisticsViewModel>();
            foreach (ServantStatisticsData data in statisticsData)
            {
                ServantStatisticsViewModel vmInner = new ServantStatisticsViewModel
                {
                    HeroImageURL = ContentURL.GetHeroIconURL(data.HeroUnitTypeID),
                    AvgDamageDealt = $"{data.AvgDamageDealt:n0}",
                    AvgDamageTaken = $"{data.AvgDamageTaken:n0}",
                    AvgGoldSpent = $"{data.AvgGoldSpent:n0}",
                    HeroName = data.HeroName,
                    HeroTitle = data.HeroTitle,
                    PlayCount = data.WinCount + data.LossCount,
                    WinRatioVal = data.WinCount * 1.0 / (data.WinCount + data.LossCount),
                    ProgressBarColor = data.WinCount < data.LossCount || data.WinCount == 0 ? "progressbar red" : "progressbar blue",
                    AvgKDA = ((data.AvgKills + data.AvgAssists)/data.AvgDeaths).ToString("0.00")
                };
                vmInner.WinRatio = $"{vmInner.WinRatioVal:0.0%}";
                vm.Add(vmInner);
            }
            vm = vm.OrderByDescending(x => x.WinRatioVal).ThenByDescending(y=>y.PlayCount).ToList();
            return vm;
        }

        private StatisticsDAL.PStatOrderType GetPlayerStatOrderType(int orderType)
        {
            switch (orderType)
            {
                case 1:
                    return StatisticsDAL.PStatOrderType.WinRate;
                case 2:
                    return StatisticsDAL.PStatOrderType.KDA;
                case 3:
                    return StatisticsDAL.PStatOrderType.DamageDealt;
                case 4:
                    return StatisticsDAL.PStatOrderType.DamageTaken;
                default:
                    throw new ArgumentOutOfRangeException("Invalid order type given: " + orderType);
            }
        }

        private StatisticsDAL.PStatTime GetPlayerStatTimeFilter(int time)
        {
            switch (time)
            {
                case 1:
                    return StatisticsDAL.PStatTime.LastSevenDays;
                case 2:
                    return StatisticsDAL.PStatTime.LastThirtyDays;
                case 3:
                    return StatisticsDAL.PStatTime.AllTime;
                default:
                    throw new ArgumentOutOfRangeException("Invalid time filter given: " + time);
            }
        }

        public List<PlayerStatisticsViewModel> GetPlayerStatistics(int orderType, int time)
        {
            List<PlayerStatisticsData> playerStatisticsData = _statisticsDal.GetPlayerStatistics(GetPlayerStatOrderType(orderType),GetPlayerStatTimeFilter(time));
            List<PlayerStatisticsViewModel> vm = new List<PlayerStatisticsViewModel>();
            foreach (PlayerStatisticsData data in playerStatisticsData)
            {
                PlayerStatisticsViewModel vmInner = new PlayerStatisticsViewModel
                {
                    PlayerName = data.PlayerName,
                    WinRatio = $"{data.WinRate:0.0%}",
                    WinRatioPBColor = data.Wins < data.Losses|| data.Wins == 0 ? "progressbar red" : "progressbar blue",
                    AvgKDA = data.KDA.ToString("0.00"),
                    AvgDamageDealt = $"{data.AvgDamageDealt:n0}",
                    AvgDamageTaken = $"{data.AvgDamageTaken:n0}",
                    AvgGoldSpent = $"{data.AvgGoldSpent:n0}",
                    KDAColor = CSSColorizer.GetKDAColor(data.KDA)
                };
                vm.Add(vmInner);
            }
            return vm;
        } 
    }
}
