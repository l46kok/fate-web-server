using System.Globalization;
using System.IO;
using System.Reflection;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using FateWebServer.Utility;
using Nancy;
using Nancy.Responses;

namespace FateWebServer.Controllers
{
    public class FileDownloadModule : NancyModule
    {
        private static readonly GameSL gameSl = GameSL.Instance;
        public FileDownloadModule()
        {
            Get["/download/replay/{gameId}"] = param =>
            {
                GameReplayData replayData = gameSl.GetReplayData(param.gameId);
                if (replayData == null || !File.Exists(replayData.ReplayPath))
                    return Response.AsError(HttpStatusCode.NotFound, "Replay file cannot be found from the server");
                var file = new FileStream(replayData.ReplayPath, FileMode.Open);
                string fileName = "Fate Another Replay " + replayData.PlayedDateTime.ToString(CultureInfo.InvariantCulture) + ".w3g";

                var response = new StreamResponse(() => file, MimeTypes.GetMimeType(fileName));
                return response.AsAttachment(fileName);
            };
        }
    }
}
