using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Extension;
using Fate.DB.DAL.FRS;
using Fate.WebServiceLayer.ViewModels;
using System.Text.RegularExpressions;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class ServantDetailSL
    {
        private readonly StatisticsDAL _detailDal;

        public ServantDetailSL(StatisticsDAL detailDal)
        {
            _detailDal = detailDal;
        }

        public Dictionary<string, int> BuildVersionPlayedData(Dictionary<string, int> versionPlayed)
        {
            Dictionary<string, int> dataModel = new Dictionary<string, int>();

            foreach (var versionData in versionPlayed)
            {
                string[] versionStrings = versionData.Key.Split(new[] { "Ver " }, StringSplitOptions.None);
                string versionString = versionStrings[1];
                string[] versionNumbers = versionString.Split('.');
                string versionMainNumber = versionNumbers[0];
                string versionSubNumber = Regex.Replace(versionNumbers[1], "[^0-9]+", string.Empty);
                string version = versionMainNumber + "." + versionSubNumber;
                if (dataModel.ContainsKey(version))
                {
                    dataModel[version] += versionData.Value;
                }
                else
                {
                    dataModel[version] = versionData.Value;
                }
                
            }

            return dataModel;
        }

        public List<ServantDetailData> BuildServantDetailData(List<ServantDetailData> servantDetails)
        {
            List<ServantDetailData> dataModel = new List<ServantDetailData>();

            foreach (var versionData in servantDetails)
            {

                string[] versionStrings = versionData.MapVersion.Split(new[] { "Ver " }, StringSplitOptions.None);
                string versionString = versionStrings[1];
                string[] versionNumbers = versionString.Split('.');
                string versionMainNumber = versionNumbers[0];
                string versionSubNumber = Regex.Replace(versionNumbers[1], "[^0-9]+", string.Empty);
                string version = versionMainNumber + "." + versionSubNumber;
                int index = dataModel.FindIndex(c => c.MapVersion.Contains(version));
                if (index != -1)
                {
                    dataModel[index].PlayCount += versionData.PlayCount;
                    dataModel[index].WinCount += versionData.WinCount;
                    dataModel[index].KillCount += versionData.KillCount;
                    dataModel[index].AssistCount += versionData.AssistCount;
                    dataModel[index].DeathCount += versionData.DeathCount;
                    dataModel[index].DamageTaken += versionData.DamageTaken;
                    dataModel[index].DamageDealt += versionData.DamageDealt;
                    dataModel[index].GoldSpent += versionData.GoldSpent;
                    dataModel[index].ScoreDifference += versionData.ScoreDifference;
                    dataModel[index].GameDuration += versionData.GameDuration;
                }
                else
                {
                    ServantDetailData servantDetail = new ServantDetailData();
                    servantDetail.PlayCount = versionData.PlayCount;
                    servantDetail.KillCount = versionData.KillCount;
                    servantDetail.AssistCount = versionData.AssistCount;
                    servantDetail.DeathCount = versionData.DeathCount;
                    servantDetail.DamageTaken = versionData.DamageTaken;
                    servantDetail.DamageDealt = versionData.DamageDealt;
                    servantDetail.GoldSpent = versionData.GoldSpent;
                    servantDetail.ScoreDifference = versionData.ScoreDifference;
                    servantDetail.GameDuration = versionData.GameDuration;
                    servantDetail.HeroTitle = versionData.HeroTitle;
                    servantDetail.WinCount = versionData.WinCount;
                    servantDetail.MapVersion = version;
                    dataModel.Add(servantDetail);
                }
            }

            dataModel = dataModel.OrderByDescending(c => Convert.ToInt32(c.MapVersion.Split('.')[0]))
                                       .ThenByDescending(c => Convert.ToInt32(c.MapVersion.Split('.')[1])).ToList();

            return (dataModel.Count > 5) ? dataModel.GetRange(0, 5) : dataModel;
        }

        public List<ServantStatisticsDetailViewModel> GetServantDetail(int heroId)
        {
            List<ServantStatisticsDetailViewModel> vm = new List<ServantStatisticsDetailViewModel>();
            List<ServantDetailData> servantDetailVersionData = this.BuildServantDetailData(_detailDal.GetServantDetailStatistics(heroId));
            Dictionary<string, int> totalGamesPlayedVersion = this.BuildVersionPlayedData(_detailDal.GetTotalGamesPlayedVersion());

            foreach (var versionData in servantDetailVersionData)
            {
                ServantStatisticsDetailViewModel vmu = new ServantStatisticsDetailViewModel();
                int totalPlayed = totalGamesPlayedVersion[versionData.MapVersion];
                decimal winRate = Convert.ToDecimal(versionData.WinCount * 100 / versionData.PlayCount);
                vmu.WinRate = $"{Math.Round(winRate, 1)}";
                decimal pickRate = Convert.ToDecimal(versionData.PlayCount * 100/ totalPlayed);
                vmu.PickRate = $"{Math.Round(pickRate, 1)}";
                int avgKills = versionData.KillCount / versionData.PlayCount;
                vmu.AvgKills = $"{avgKills}";
                int avgDeaths = versionData.DeathCount / versionData.PlayCount;
                vmu.AvgDeaths = $"{avgDeaths}";
                int avgAssists = versionData.AssistCount / versionData.PlayCount;
                vmu.AvgAssists = $"{avgAssists}";
                decimal avgKDA = Convert.ToDecimal(versionData.AssistCount + versionData.KillCount) / versionData.DeathCount;
                vmu.AvgKDA = $"{Math.Round(avgKDA, 2)}";
                vmu.PlayCount = versionData.PlayCount;
                vmu.MapVersion = versionData.MapVersion;
                int avgGoldSpent = Convert.ToInt32(versionData.GoldSpent / versionData.PlayCount);
                vmu.AvgGoldSpent = $"{avgGoldSpent}";
                int avgDamageDealt = Convert.ToInt32(versionData.DamageDealt / versionData.PlayCount);
                vmu.AvgDamageDealt = $"{avgDamageDealt}";
                int avgDamageTaken = Convert.ToInt32(versionData.DamageTaken / versionData.PlayCount);
                vmu.AvgDamageTaken = $"{avgDamageTaken}";
                TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(versionData.GameDuration / versionData.PlayCount));
                string timeString = time.ToString(@"hh\:mm\:ss");
                vmu.AvgGameDuration = timeString;
                decimal avgScoreDifference = Convert.ToDecimal(versionData.ScoreDifference / versionData.PlayCount);
                vmu.AvgScoreDifference = $"{Math.Round(avgScoreDifference, 1)}";
                vm.Add(vmu);
            }
            return vm;
        }
    }
}
