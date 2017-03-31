using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ServantDetailModule : NancyModule
    {
        public ServantDetailModule(ServantDetailSL servantDetailSl)
        {
            Get["/ServantDetails/{servantId}"] = param =>
            {
                int servantIdParam = param.servantId;
                List<ServantStatisticsDetailViewModel> data = servantDetailSl.GetServantDetail(servantIdParam);
                return Response.AsJson(data);
            };
        }
    }
}
