using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fate.DB.DAL.FRS;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class BanListSL
    {
        private readonly BanDAL _banDal;

        public BanListSL(BanDAL banDal)
        {
            _banDal = banDal;
        }

        public BanListViewModel GetBannedPlayers()
        {
            var playerBanDataList = _banDal.GetCurrentlyBannedPlayers(false).ToList();
            BanListViewModel banListViewModel = new BanListViewModel
            {
                PermanentBans = playerBanDataList.Where(x => x.IsPermanentBan),
                TimeBans = playerBanDataList.Where(x => !x.IsPermanentBan).ToList()
            };
            banListViewModel.TimeBans.ToList().ForEach(x =>
            {
                if (x.BannedUntil != null)
                    x.RemainingDuration = (int) x.BannedUntil.Value.Subtract(DateTime.Now).TotalMinutes;
            });
            return banListViewModel;
        }
    }
}
