using Fate.Common.Data;
using Fate.WebServiceLayer;
using Nancy;

namespace FateWebServer.Controllers
{
    public class GameDetailModule : NancyModule
    {
        private static readonly GameDetailSL gameDetailSl = new GameDetailSL();
        public GameDetailModule()
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
