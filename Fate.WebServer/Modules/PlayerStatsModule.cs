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
        public PlayerStatsModule(PlayerStatSL playerStatsSl, StatisticsSL statisticsSl)
        {
            Get["/PlayerStatsAjax/{server}/{playerName}/{gameId}/{heroUnitTypeId}"] = param =>
            {
                List<PlayerGameSummaryViewModel> gameSummaryData = playerStatsSl.GetPlayerGameSummary(param.PlayerName, param.server, param.gameId, param.heroUnitTypeId);
                return Response.AsJson(gameSummaryData);
            };

            Get["/PlayerStatsAjax/Statistics/{orderType}/{time}"] = param =>
            {
                List<PlayerStatisticsViewModel> playerStatistics = statisticsSl.GetPlayerStatistics(param.orderType,param.time);
                return Response.AsJson(playerStatistics);
            };
        }
    }
}
