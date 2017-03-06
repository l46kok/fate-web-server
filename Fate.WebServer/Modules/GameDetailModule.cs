using System.Diagnostics.CodeAnalysis;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Nancy;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class GameDetailModule : NancyModule
    {
        public GameDetailModule(GameDetailSL gameDetailSl)
        {
            Get["/gameDetail/{gameId}"] = param =>
            {
                int gameIdParam = param.gameId;
                GameDetailData data = gameDetailSl.GetGameDetail(gameIdParam);
                return Response.AsJson(data);
            };
        }
    }
}
