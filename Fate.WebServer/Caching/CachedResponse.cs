using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace FateWebServer.Caching
{
    /// <summary>
    /// Wraps a regular response in a cached response
    /// The cached response invokes the old response and stores it as a string.
    /// Obviously this only works for ASCII text based responses, so don't use this
    /// in a real application :-)
    /// </summary>
    public class CachedResponse : Response
    {
        private readonly Response response;

        public CachedResponse(Response response)
        {
            this.response = response;

            ContentType = response.ContentType;
            Headers = response.Headers;
            StatusCode = response.StatusCode;
            Contents = GetContents();
        }

        public override Task PreExecute(NancyContext context)
        {
            return response.PreExecute(context);
        }

        private Action<Stream> GetContents()
        {
            return stream =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    response.Contents.Invoke(memoryStream);

                    var contents =
                        Encoding.UTF8.GetString(memoryStream.GetBuffer());

                    var writer =
                        new StreamWriter(stream) { AutoFlush = true };

                    writer.Write(contents);
                }
            };
        }
    }
}
