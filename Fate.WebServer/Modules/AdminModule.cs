using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Extension;
using Fate.Common.Utility;
using Fate.DB;
using Fate.WebServiceLayer;
using Fate.WebServiceLayer.ViewModels;
using Nancy;
using Nancy.Security;

namespace FateWebServer.Modules
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class AdminModule : NancyModule
    { 
        public AdminModule(AdminBanSL adminBanSl, AdminSearchSL adminSearchSl)
        {
            this.RequiresAuthentication();
            this.RequiresClaims("Admin");
            
            Get["/Admin"] = param => View["Views/Admin/AdminConsole.sshtml"];
            Get["/Admin/PlayerSearch"] = param => View["Views/Admin/AdminSearch.sshtml"];
            Get["/Admin/Search/{filterType}/{filterInput}"] = param =>
            {
                string filterInput = param.filterInput;
                int filterType = param.filterType;
                int filterRange = param.filterRange;
                string filterRangeInput = param.filterRangeInput;
                //TODO: Currently only handles one database (ASIA)
                ghostEntities.InitDatabaseConnection(ConfigHandler.GhostDatabaseList.First());
                switch (filterType)
                {
                    case 1:
                        return Response.AsJson(adminSearchSl.SearchByPlayerName(filterInput, filterRange, filterRangeInput));
                    case 2:
                        return Response.AsJson(adminSearchSl.SearchByIp(filterInput));
                    default:
                        return Response.AsError(HttpStatusCode.BadRequest, "Invalid FilterType");
                }
            };
            Get["/Admin/Ban"] = param => View["Views/Admin/AdminBan.sshtml"];
            Post["/Admin/Ban"] = param =>
            {
                int banType = Request.Form.banType;
                DateTime bannedUntil;
                switch (banType)
                {
                    case 1:
                        try
                        {
                            int banDuration = Request.Form.banDuration;
                            bannedUntil = DateTime.Now.AddMinutes(banDuration);
                        }
                        catch
                        {
                            return View["Views/Admin/AdminBan.sshtml", GetBanResultViewModel(false, "You must specify a valid duration.")];
                        }
                        break;
                    case 2:
                        bannedUntil = DateTime.MaxValue;
                        break;
                    default:
                        return View["Views/Admin/AdminBan.sshtml", GetBanResultViewModel(false, "You must select a Ban Type.")];
                }

                string ipAddressInline = Request.Form.banIpAddresses;
                List<string> ipAddresses = ipAddressInline.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(x=>x.Trim()).ToList();

                PlayerBanData playerBanData = new PlayerBanData()
                {
                    Admin = Context.CurrentUser.UserName,
                    BannedUntil = bannedUntil,
                    IpAddresses = ipAddresses,
                    IsPermanentBan = banType == 2,
                    PlayerName = Request.Form.banPlayerName,
                    Reason = Request.Form.banReason
                };

                playerBanData.PlayerName = playerBanData.PlayerName.Trim();

                string validationMessage = adminBanSl.ValidateBanForm(playerBanData);
                if (!String.IsNullOrEmpty(validationMessage))
                {
                    return View["Views/Admin/AdminBan.sshtml", GetBanResultViewModel(false, validationMessage)];
                }

                if (adminBanSl.BanPlayer(playerBanData, ConfigHandler.GhostDatabaseList))
                {
                    return View["Views/Admin/AdminBan.sshtml", GetBanResultViewModel(true, $"{playerBanData.PlayerName} has been struck down with the banhammer.")];
                }

                return View["Views/Admin/AdminBan.sshtml", GetBanResultViewModel(false, "Ban Failure. The player might already be banned or it might be a temporary error. Check and try again.")];
            };
        }

        private AdminBanViewModel GetBanResultViewModel(bool success, string message)
        {
            AdminBanViewModel banViewModel = new AdminBanViewModel
            {
                HasPostedBan = true,
                Alert = success ? "alert-success" : "alert-danger",
                BanResult = message
            };
            return banViewModel;
        }
    }
}

