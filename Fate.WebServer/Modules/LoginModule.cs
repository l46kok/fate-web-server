using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fate.Common.Data;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Linker;
using Nancy.ModelBinding;

namespace FateWebServer.Modules
{

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LoginModule : NancyModule
    {
        private static readonly LoginSL loginSl = LoginSL.Instance;

        public LoginModule(IResourceLinker linker)
        {
            Post["/CreateAccount"] = param =>
            {
                WebUserData data = this.Bind<WebUserData>();
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
                WebUserData data = this.Bind<WebUserData>();
                if (!loginSl.IsAccountNameRegistered(data.UserName))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "You have specified an incorrect or inactive username, or an invalid password.";
                    return View["Views/Login.sshtml", loginViewModel];
                }

                if (!loginSl.IsUserAdmin(data.UserName))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "You need to be an admin to access this page.";
                    return View["Views/Login.sshtml", loginViewModel];
                }

                data.Claims = new List<string> { "Admin" };
                Guid guid = Guid.NewGuid();
                if (!loginSl.LoginWithCredentials(data, guid))
                {
                    loginViewModel.HasError = true;
                    loginViewModel.ErrorMessage = "You have specified an incorrect or inactive username, or an invalid password.";
                    return View["Views/Login.sshtml", loginViewModel];
                }

                DateTime expiry = DateTime.Now;
                expiry = data.Remember ? expiry.AddDays(7) : expiry.AddHours(1);

                return this.LoginAndRedirect(guid, expiry, "/Admin");
            };
        }
    }
}
