using System;
using System.Collections.Generic;
using System.Linq;
using Fate.Common.Data;
using Fate.DB.DAL;
using Fate.WebServiceLayer.Extension;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    public class GameDetailSL
    {
        private static readonly GameDetailDAL _detailDal = new GameDetailDAL();
        private static readonly GameDetailSL _instance = new GameDetailSL();
        public static GameDetailSL Instance => _instance;

        private GameDetailSL() { }

        private void PopulateImageURL(GamePlayerDetailData data)
        {
            data.HeroImageURL = ContentURL.GetHeroIconURL(data.HeroUnitTypeID);
            data.GodsHelpImageURLList = new List<string>();
            foreach (string url in data.GodsHelpAbilIDList.Where(x=>!String.IsNullOrEmpty(x))
                                                          .Select(ContentURL.GetGodsHelpIconURL)
                                                          .Where(url => !String.IsNullOrEmpty(url)))
            {
                data.GodsHelpImageURLList.Add(url);
            }
        }
        public GameDetailData GetGameDetail(int gameId)
        {
            GameDetailData dataModel = new GameDetailData();
            List<GamePlayerDetailData> gameDetailData = _detailDal.GetGameDetails(gameId);
            dataModel.Team1Data = gameDetailData.Where(x => x.Team == "1").ToList();
            dataModel.Team2Data = gameDetailData.Where(x => x.Team == "2").ToList();
            foreach (GamePlayerDetailData data in dataModel.Team1Data)
            {
                dataModel.Team1Kills += data.Kills;
                dataModel.Team1Deaths += data.Deaths;
                dataModel.Team1Assists += data.Assists;
                dataModel.Team1Gold += data.GoldSpent;
                dataModel.Team1DamageDealt += data.DamageDealt;
                PopulateImageURL(data);
            }
            foreach (GamePlayerDetailData data in dataModel.Team2Data)
            {
                dataModel.Team2Kills += data.Kills;
                dataModel.Team2Deaths += data.Deaths;
                dataModel.Team2Assists += data.Assists;
                dataModel.Team2Gold += data.GoldSpent;
                dataModel.Team2DamageDealt += data.DamageDealt;
                PopulateImageURL(data);
            }
            return dataModel;
        }


        public PlayerGameBuildViewModel GetPlayerGameBuild(int gameId, string playerName)
        {
            PlayerGameBuildViewModel vm = new PlayerGameBuildViewModel();
            PlayerGameBuildData data = _detailDal.GetPlayerGameBuildDetail(playerName, gameId);
            //TO DO: Is there any cleaner way of doing this?
            vm.PlayerName = playerName;
            vm.HeroIconURL = ContentURL.GetHeroIconURL(data.HeroUnitTypeId);
            vm.Strength = data.StatBuildDic.GetValueOrDefault("A02W");
            vm.Agility = data.StatBuildDic.GetValueOrDefault("A03D");
            vm.Intelligence = data.StatBuildDic.GetValueOrDefault("A03E");
            vm.Attack = data.StatBuildDic.GetValueOrDefault("A03W");
            vm.Armor = data.StatBuildDic.GetValueOrDefault("A03X");
            vm.HealthRegen = data.StatBuildDic.GetValueOrDefault("A03Y");
            vm.ManaRegen = data.StatBuildDic.GetValueOrDefault("A03Z");
            vm.MoveSpeed = data.StatBuildDic.GetValueOrDefault("A04Y");
            vm.GoldRegen = data.StatBuildDic.GetValueOrDefault("A0A9");
            vm.PrelatiMana = data.StatBuildDic.GetValueOrDefault("A0CJ");
            vm.WardCount += data.WardFamiliarDic.GetValueOrDefault("I003");
            vm.WardCount += data.WardFamiliarDic.GetValueOrDefault("I00N");
            vm.WardCount += data.WardFamiliarDic.GetValueOrDefault("SWAR");
            vm.FamiliarCount += data.WardFamiliarDic.GetValueOrDefault("I002");
            vm.FamiliarCount += data.WardFamiliarDic.GetValueOrDefault("I005");
            return vm;
        }
    }
}
