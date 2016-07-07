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

            Conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/", "Assets")
            );
        }

        protected override byte[] FavIcon
        {
            /*get { return this.favicon ?? (this.favicon = null); }*/
            get { return null; }
        }

        private byte[] LoadFavIcon()
        {
            using (var resourceStream = GetType().Assembly.GetManifestResourceStream("AssemblyName.favicon.ico"))
            {
                var memoryStream = new MemoryStream();
                resourceStream.CopyTo(memoryStream);
                return memoryStream.GetBuffer();
            }
        }
    }
}
