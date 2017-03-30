using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Autofac;
using Fate.Common.Extension;
using Fate.DB.DAL.FRS;
using Fate.DB.DAL.GHost;
using Fate.WebServiceLayer;
using FateWebServer.Caching;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;

namespace FateWebServer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MainBootstrapper : AutofacNancyBootstrapper
    {
        public static bool IsMaintenanceMode { get; set; }

        private readonly Dictionary<string, Tuple<DateTime, Response, int>> _cachedResponses = new Dictionary<string, Tuple<DateTime, Response, int>>();
        private byte[] favicon;

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.BeforeRequest += BeforeRequestHandler;
            pipelines.AfterRequest += SetCache;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            // Container registrations for service layers
            container.Update(builder => builder.RegisterType<LoginSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<GameSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<PlayerStatSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<StatisticsSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<GameDetailSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<BanListSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<AdminSearchSL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<ServantDetailSL>().AsSelf().SingleInstance());
            // These two are dependant on each other, so we allow circular dependencies
            container.Update(builder => builder.RegisterType<GhostCommSL>().AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance());
            container.Update(builder => builder.RegisterType<AdminBanSL>().AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance());

            // Container registrations for data access layers
            container.Update(builder => builder.RegisterType<BanDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<GameDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<GameDetailDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<LoginDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<PlayerStatDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<ServantSearchDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<StatisticsDAL>().AsSelf().SingleInstance());
            container.Update(builder => builder.RegisterType<GHostPlayerDAL>().AsSelf().SingleInstance());
        }

        protected override void RequestStartup(ILifetimeScope requestContainer, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(requestContainer, pipelines, context);

            if (context.Request.Url.Path.StartsWith("/Login"))
            {
                var formsAuthConfiguration =
                    new FormsAuthenticationConfiguration()
                    {
                        RedirectUrl = "/Login",
                        UserMapper = requestContainer.Resolve<LoginSL>(),
                    };

                FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
            }

            if (context.Request.Url.Path.StartsWith("/Admin"))
            {
                var formsAuthConfiguration =
                    new FormsAuthenticationConfiguration()
                    {
                        RedirectUrl = "/Login",
                        UserMapper = requestContainer.Resolve<LoginSL>(),
                    };

                FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
            }
        }

        /// <summary>
        /// Check to see if we have a cache entry - if we do, see if it has expired or not,
        /// if it hasn't then return it, otherwise return null;
        /// </summary>
        /// <param name="context">Current context</param>
        /// <returns>Request or null</returns>
        private Response BeforeRequestHandler(NancyContext context)
        {
#if (!DEBUG)
            if (IsMaintenanceMode)
            {
                Response maintenanceResponse = new Response {StatusCode = HttpStatusCode.ServiceUnavailable};
                return maintenanceResponse;
            }

            Tuple<DateTime, Response, int> cacheEntry;

            if (_cachedResponses.TryGetValue(context.Request.Path, out cacheEntry))
            {
                if (cacheEntry.Item1.AddSeconds(cacheEntry.Item3) > DateTime.Now)
                {
                    return cacheEntry.Item2;
                }
            }
#endif
            return null;
        }

        /// <summary>
        /// Adds the current response to the cache if required
        /// Only stores by Path and stores the response in a dictionary.
        /// Do not use this as an actual cache :-)
        /// </summary>
        /// <param name="context">Current context</param>
        private void SetCache(NancyContext context)
        {
            if (context.Response.StatusCode != HttpStatusCode.OK)
            {
                return;
            }

            object cacheSecondsObject;
            if (!context.Items.TryGetValue(ContextExtensions.OUTPUT_CACHE_TIME_KEY, out cacheSecondsObject))
            {
                return;
            }

            int cacheSeconds;
            if (!int.TryParse(cacheSecondsObject.ToString(), out cacheSeconds))
            {
                return;
            }

            var cachedResponse = new CachedResponse(context.Response);

            _cachedResponses[context.Request.Path] = new Tuple<DateTime, Response, int>(DateTime.Now, cachedResponse, cacheSeconds);

            context.Response = cachedResponse;
        }

        protected override IRootPathProvider RootPathProvider => new CustomRootPathProvider();

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            StaticConfiguration.DisableErrorTraces = false;
            favicon = LoadFavIcon();
        }

        protected override byte[] FavIcon => favicon ?? (favicon = null);

        private byte[] LoadFavIcon()
        {
            using (var resourceStream = GetType().Assembly.GetManifestResourceStream("FateWebServer.favicon.ico"))
            {
                var memoryStream = new MemoryStream();
                Debug.Assert(resourceStream != null, "resourceStream != null");
                resourceStream.CopyTo(memoryStream);
                return memoryStream.GetBuffer();
            }
        }
    }
}
