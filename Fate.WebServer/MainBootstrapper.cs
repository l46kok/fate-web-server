using System;
using System.Collections.Generic;
using System.IO;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using FateWebServer.Caching;
using FateWebServer.Controllers;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace FateWebServer
{
    public class MainBootstrapper : DefaultNancyBootstrapper
    {
        public static bool IsMaintenanceMode { get; set; } = false;

        private readonly Dictionary<string, Tuple<DateTime, Response, int>> cachedResponses = new Dictionary<string, Tuple<DateTime, Response, int>>();
        private byte[] favicon;

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.BeforeRequest += BeforeRequestHandler;
            pipelines.AfterRequest += SetCache;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            // We don't call "base" here to prevent auto-discovery of
            // types/dependencies
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<IUserMapper, LoginSL>();
        }

        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
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
            if (IsMaintenanceMode)
            {
                Response maintenanceResponse = new Response {StatusCode = HttpStatusCode.ServiceUnavailable};
                return maintenanceResponse;
            }
#if DEBUG
            return null;
#endif

            Tuple<DateTime, Response, int> cacheEntry;

            if (this.cachedResponses.TryGetValue(context.Request.Path, out cacheEntry))
            {
                if (cacheEntry.Item1.AddSeconds(cacheEntry.Item3) > DateTime.Now)
                {
                    return cacheEntry.Item2;
                }
            }

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

            cachedResponses[context.Request.Path] = new Tuple<DateTime, Response, int>(DateTime.Now, cachedResponse, cacheSeconds);

            context.Response = cachedResponse;
        }

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
            get { return favicon ?? (favicon = null); }
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
