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
            DirectoryInfo debugDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            return debugDir.Parent.Parent.FullName;
#else
        return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
#endif
        }
    }
}
