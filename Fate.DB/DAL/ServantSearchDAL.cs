﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate.Common.Data;

namespace Fate.DB.DAL
{
    public class ServantSearchDAL
    {
        public List<SearchableServantData> GetSearchableServants()
        {
            using (frsDb db = frsDb.Create())
            {
                return (
                        from heroType in db.herotype
                        join herotypename in db.herotypename on heroType.HeroTypeID equals herotypename.FK_HeroTypeID
                        select new SearchableServantData()
                        {
                            HeroUnitTypeID = heroType.HeroUnitTypeID,
                            HeroNameTitle = herotypename.HeroName + " - " + herotypename.HeroTitle
                        }
                     ).ToList();
            }
        }
    }
}