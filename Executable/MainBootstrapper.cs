using Nancy;
using Nancy.Conventions;

namespace FateWebServer
{
    public class MainBootstrapper : DefaultNancyBootstrapper
    {
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
    }
}
