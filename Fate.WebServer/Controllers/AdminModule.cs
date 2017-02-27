using System.Linq;
using Fate.DB;
using Fate.WebServiceLayer;
using FateWebServer.Utility;
using Nancy;
using Nancy.Security;

namespace FateWebServer.Controllers
{
    public class AdminModule : NancyModule
    {
        private readonly AdminSearchPlayerDataSL _adminSearchPlayerDataSl = AdminSearchPlayerDataSL.Instance;
        public AdminModule()
        {
#if DEBUG
            this.RequiresAuthentication();
#endif

            Get["/Admin"] = param => View["Views/Admin/AdminConsole.sshtml"];
            Get["/Admin/PlayerSearch"] = param => View["Views/Admin/AdminSearch.sshtml"];
            Get["/Admin/PlayerSearch/{playerName}"] = param =>
            {
                string playerName = param.playerName;
                //TODO: Currently only handles one database (ASIA)
                ghostEntities.InitDatabaseConnection(ConfigHandler.GhostDatabaseList.First());
                return Response.AsJson(_adminSearchPlayerDataSl.GetPlayerSearchData(playerName));
            };
            Get["/AdminBan"] = param => View["Views/Admin/AdminBan.sshtml"];
        }
    }
}

