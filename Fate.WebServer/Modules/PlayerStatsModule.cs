using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class PlayerStatsModule : NancyModule
    {
        private static readonly PlayerStatSL _playerStatsSl = PlayerStatSL.Instance;
        private static readonly StatisticsSL _statisticsSl = StatisticsSL.Instance;
        public PlayerStatsModule()
        {
            Get["/PlayerStatsAjax/{server}/{playerName}/{gameId}/{heroUnitTypeId}"] = param =>
            {
                List<PlayerGameSummaryViewModel> gameSummaryData = _playerStatsSl.GetPlayerGameSummary(param.PlayerName, param.server, param.gameId, param.heroUnitTypeId);
                return Response.AsJson(gameSummaryData);
            };

            Get["/PlayerStatsAjax/Statistics/{orderType}/{time}"] = param =>
            {
                List<PlayerStatisticsViewModel> playerStatistics = _statisticsSl.GetPlayerStatistics(param.orderType,param.time);
                return Response.AsJson(playerStatistics);
            };
        }
    }
}
