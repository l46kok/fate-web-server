using System;
using System.Diagnostics.CodeAnalysis;
using Fate.Common.Extension;
using Fate.Common.Utility;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Linker;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MainModule : NancyModule
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public MainModule(IResourceLinker linker, GameSL gameSl,
                          PlayerStatSL playerStatsSl, GhostCommSL ghostCommSl,
                          StatisticsSL statisticsSl, GameDetailSL gameDetailSl,
                          BanListSL banListSl)
        {
            Get["/"] = _ =>
            {
                Context.EnableOutputCache(15);
                MainPageViewModel mpVm = new MainPageViewModel
                {
                    CurrentBotTime = DateTime.Now,
                    RecentGameDataList = gameSl.GetRecentGames(10)
                };
                return View["Views/MainPage.sshtml", mpVm];
            };

            Post["/Search"] = param => Response.AsRedirect($"/PlayerStats/USEast/{Request.Form.searchPlayerName}");

            Get["/GameList"] = param =>
            {
                Context.EnableOutputCache(10);
                return View["Views/GameList.sshtml", ghostCommSl.GetGameList()];
            };

            Get["/About"] = Param => View["Views/About.sshtml"];

            Get["/ServantStatistics"] = Param =>
            {
                Context.EnableOutputCache(30);
                return View["Views/ServantStatistics.sshtml", statisticsSl.GetServantStatistics()];
            };

            Get["/LeaderBoards"] = Param =>
            {
                Context.EnableOutputCache(30);
                return View["Views/LeaderBoards.sshtml"];
            };

            Get["/Downloads"] = Param => View["Views/Downloads.sshtml", ConfigHandler.MapName];

            Get["/PlayerGameBuildDetail/{GameID}/{PlayerName}"] = param =>
            {
                PlayerGameBuildViewModel vm = gameDetailSl.GetPlayerGameBuild(param.GameID, param.PlayerName);
                return View["Views/PlayerGameBuildDetail.sshtml",vm];
            };

            Get["/Log"] = x =>
            {
                string gameLog = gameSl.GetGameLog(Request.Query.gameId);
                return View["Views/Log.sshtml",gameLog];
            };
            Get["/PlayerStats/{server}/{playerName}"] = param =>
            {
                PlayerStatsPageViewModel summaryData = playerStatsSl.GetPlayerStatSummary(param.playerName, param.server, int.MaxValue);
                return View["Views/PlayerStatsPage.sshtml", summaryData];
            };

            Get["/Maintenance"] = param => View["Views/Maintenance.sshtml"];

            Get["/Login"] = param => View["Views/Login.sshtml", new LoginViewModel()];

            Get["/BanList"] = param =>
            {
                Context.EnableOutputCache(60);
                return View["Views/BanList.sshtml", banListSl.GetBannedPlayers()];
            };
        }
    }
}
