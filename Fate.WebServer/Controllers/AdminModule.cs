using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Responses;
using Nancy.Security;
using Nancy.TinyIoc;

namespace FateWebServer.Controllers
{
    public class AdminModule : NancyModule
    {
        public AdminModule()
        {

            // Before += ctx => (Context.CurrentUser == null) ? new HtmlResponse(HttpStatusCode.Unauthorized) : null;
            this.RequiresAuthentication();
            
            Get["/Admin"] = param => View["Views/AdminConsole.sshtml"];
        }
    }
}

