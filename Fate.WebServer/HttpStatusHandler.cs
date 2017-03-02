using System.Diagnostics.CodeAnalysis;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace FateWebServer
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class HttpStatusHandler : IStatusCodeHandler
    {
        private readonly IViewRenderer _viewRenderer;

        public HttpStatusHandler(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode,
                                      NancyContext context)
        {
            return statusCode == HttpStatusCode.ServiceUnavailable;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = _viewRenderer.RenderView(context, "/Maintenance");
            response.StatusCode = statusCode;
            context.Response = response;
        }
    }
}
