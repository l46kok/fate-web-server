//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fate.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class herostatlearn
    {
        public int HeroStatLearnID { get; set; }
        public int FK_HeroStatInfoID { get; set; }
        public int FK_GamePlayerDetailID { get; set; }
        public int LearnCount { get; set; }
    }
}
