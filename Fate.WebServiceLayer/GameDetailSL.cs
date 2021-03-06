﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fate.Common.Data;
using Fate.Common.Extension;
using Fate.DB.DAL.FRS;
using Fate.WebServiceLayer.ViewModels;

namespace Fate.WebServiceLayer
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class GameDetailSL
    {
        private readonly GameDetailDAL _detailDal;

        public GameDetailSL(GameDetailDAL detailDal)
        {
            _detailDal = detailDal;
        }

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
            //Populate godshelp information first
            foreach (GamePlayerDetailData data in gameDetailData)
            {
                data.GodsHelpAbilIDList = data.GodsHelpAbilIDConcat?.Split(',').ToList() ?? new List<string>();
            }

            dataModel.Team1Data = gameDetailData.Where(x => x.Team == "1").ToList();
            dataModel.Team2Data = gameDetailData.Where(x => x.Team == "2").ToList();
            dataModel.Team1WinCount = gameDetailData.First(x => x.Team == "1").TeamOneWinCount;
            dataModel.Team2WinCount = gameDetailData.First(x => x.Team == "2").TeamTwoWinCount;
            dataModel.GameID = gameDetailData.First().GameID;
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
            vm.WardCount += data.PurchasedItemsDic.GetValueOrDefault("I003");
            vm.WardCount += data.PurchasedItemsDic.GetValueOrDefault("I00N");
            vm.WardCount += data.PurchasedItemsDic.GetValueOrDefault("SWAR");
            vm.FamiliarCount += data.PurchasedItemsDic.GetValueOrDefault("I002");
            vm.FamiliarCount += data.PurchasedItemsDic.GetValueOrDefault("I005");
            vm.SpiritLinkCount += data.PurchasedItemsDic.GetValueOrDefault("I00G");
            vm.SpiritLinkCount += data.PurchasedItemsDic.GetValueOrDefault("I00R");
            vm.FirstCS = data.CommandSealDic.GetValueOrDefault("A094");
            vm.SecondCS = data.CommandSealDic.GetValueOrDefault("A043");
            vm.ThirdCS = data.CommandSealDic.GetValueOrDefault("A044");
            vm.FourthCS = data.CommandSealDic.GetValueOrDefault("A05Q");

            vm.AttributeList = new List<PlayerGameBuildAttribute>();
            foreach (Tuple<string, string> attributeData in data.LearnedAttributeList)
            {
                PlayerGameBuildAttribute attr = new PlayerGameBuildAttribute
                {
                    AttributeImgUrl = ContentURL.GetAttributeIconURL(attributeData.Item1),
                    AttributeName = attributeData.Item2
                };
                vm.AttributeList.Add(attr);
            }
            return vm;
        }
    }
}
