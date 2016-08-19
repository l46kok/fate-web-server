using System.IO;
using System.Reflection;
using Nancy;

namespace FateWebServer
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
#if DEBUG
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
#else
        return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
#endif
        }
    }
}
