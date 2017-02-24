using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace FateWebServer.Controllers
{
    public class LoginModule : NancyModule
    {
        private static readonly LoginSL loginSl = LoginSL.Instance;

        public LoginModule()
        {
            Post["/CreateAccount"] = param =>
            {
                LoginData data = this.Bind<LoginData>();
                
                if (loginSl.IsAccountNameRegistered(data.UserName))
                {
                    return "Error";
                }

                loginSl.CreateNewAccount(data.UserName, data.Password);
                return "Success";
            };

            Post["/Login"] = param =>
            {
                LoginData data = this.Bind<LoginData>();
                if (!loginSl.IsAccountNameRegistered(data.UserName))
                {
                    return "Error";
                }

                if (!loginSl.LoginWithCredentials(data.UserName, data.Password))
                {
                    return "Error";
                }
                return "Success";
            };
        }
    }
}
