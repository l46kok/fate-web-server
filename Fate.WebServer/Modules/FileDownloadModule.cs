using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Fate.Common.Data;
using Fate.Common.Extension;
using Fate.Common.Utility;
using Fate.WebServiceLayer;
using Nancy;
using Nancy.Responses;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

            Get["/download/{fileName}"] = param => SendAttachmentResponse(param.fileName);
            Get["/download/map"] = param => SendAttachmentResponse(ConfigHandler.MapName);
        }

        private Response SendAttachmentResponse(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    $@"content\download\{fileName}");
            var file = new FileStream(filePath, FileMode.Open);
            var response = new StreamResponse(() => file, MimeTypes.GetMimeType(fileName));
            return response.AsAttachment(fileName);
        }
    }
}
