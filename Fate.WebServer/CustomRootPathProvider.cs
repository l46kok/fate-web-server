using System.IO;
using Fate.Common.Utility;
using Nancy;

namespace FateWebServer
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
#if DEBUG
            DirectoryInfo debugDir = new DirectoryInfo(PathHandler.GetAssemblyPath());
            return debugDir.Parent?.Parent?.FullName;
#else
            return PathHandler.GetAssemblyPath();
#endif
        }
    }
}
