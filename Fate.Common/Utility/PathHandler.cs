using System.IO;
using System.Reflection;

namespace Fate.Common.Utility
{
    public static class PathHandler
    {
        public static string GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}
