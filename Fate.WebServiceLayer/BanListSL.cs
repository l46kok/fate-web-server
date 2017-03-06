using System;
using System.Linq;
using Fate.DB.DAL.FRS;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    public class BanListSL
    {
        private static readonly BanDAL _banDal = new BanDAL();
        public static BanListSL Instance { get; } = new BanListSL();

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
                    x.RemainingDuration = x.BannedUntil.Value.Subtract(DateTime.Now).Minutes;
            });
            return banListViewModel;
        }
    }
}
