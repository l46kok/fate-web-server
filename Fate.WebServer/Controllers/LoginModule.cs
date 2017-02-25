using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
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
                LoginViewModel loginViewModel = new LoginViewModel();

                if (loginSl.IsAccountNameRegistered(data.UserName))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "Account name is already in use.";
                    return View["Views/Login.sshtml", loginViewModel];
                }

                loginSl.CreateNewAccount(data);
                return View["Views/Login.sshtml", loginViewModel];
            };

            Post["/Login"] = param =>
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                LoginData data = this.Bind<LoginData>();
                if (!loginSl.IsAccountNameRegistered(data.UserName))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "You have specified an incorrect or inactive username, or an invalid password.";
                    return View["Views/Login.sshtml", loginViewModel];
                }

                if (!loginSl.LoginWithCredentials(data.UserName, data.Password))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "You have specified an incorrect or inactive username, or an invalid password.";
                    return View["Views/Login.sshtml", loginViewModel];
                }
                return View["Views/Login.sshtml", loginViewModel];
            };
        }
    }
}
