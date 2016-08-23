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
    public class StatisticsSL
    {
        private static readonly StatisticsSL _instance = new StatisticsSL();
        private static readonly StatisticsDAL _statisticsDal = new StatisticsDAL();

        public static StatisticsSL Instance => _instance;

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
                    KDAColor = data.WinCount < data.LossCount || data.WinCount == 0 ? "progressbar red" : "progressbar blue",
                    AvgKDA = ((data.AvgKills + data.AvgAssists)/data.AvgDeaths).ToString("0.00")
                };
                vmInner.WinRatio = $"{vmInner.WinRatioVal:0.0%}";
                vm.Add(vmInner);
            }
            vm = vm.OrderByDescending(x => x.WinRatioVal).ThenByDescending(y=>y.PlayCount).ToList();
            return vm;
        }
    }
}
