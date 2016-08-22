using System.IO;
using Nancy;
using Nancy.Conventions;

namespace FateWebServer
{
    public class MainBootstrapper : DefaultNancyBootstrapper
    {

        private byte[] favicon;
        protected override IRootPathProvider RootPathProvider
        {
            get { return new CustomRootPathProvider(); }
        }
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            StaticConfiguration.DisableErrorTraces = false;
            favicon = LoadFavIcon();
        }

        protected override byte[] FavIcon
        {
            get { return this.favicon ?? (this.favicon = null); }
        }

        private byte[] LoadFavIcon()
        {
            using (var resourceStream = GetType().Assembly.GetManifestResourceStream("FateWebServer.favicon.ico"))
            {
                var memoryStream = new MemoryStream();
                resourceStream.CopyTo(memoryStream);
                return memoryStream.GetBuffer();
            }
        }
    }
}
