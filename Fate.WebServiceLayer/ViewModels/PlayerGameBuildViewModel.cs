using System;
using System.Collections.Generic;

namespace Fate.WebServiceLayer.ViewModels
{
    public class PlayerGameBuildViewModel
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Attack { get; set; }
        public int Armor { get; set; }
        public int HealthRegen { get; set; }
        public int ManaRegen { get; set; }
        public int MoveSpeed { get; set; }
        public int GoldRegen { get; set; }
        public int PrelatiMana { get; set; }
        public int FirstCS { get; set; }
        public int SecondCS { get; set; }
        public int ThirdCS { get; set; }
        public int FourthCS { get; set; }
        public int WardCount { get; set; }
        public int FamiliarCount { get; set; }
    }
}
