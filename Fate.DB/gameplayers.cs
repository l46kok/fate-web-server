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
    
    public partial class gameplayers
    {
        public int id { get; set; }
        public int botid { get; set; }
        public int gameid { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public int spoofed { get; set; }
        public int reserved { get; set; }
        public int loadingtime { get; set; }
        public int left { get; set; }
        public string leftreason { get; set; }
        public int team { get; set; }
        public int colour { get; set; }
        public string spoofedrealm { get; set; }
    }
}