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
    
    public partial class games
    {
        public int id { get; set; }
        public int botid { get; set; }
        public string server { get; set; }
        public string map { get; set; }
        public System.DateTime datetime { get; set; }
        public string gamename { get; set; }
        public string ownername { get; set; }
        public int duration { get; set; }
        public int gamestate { get; set; }
        public string creatorname { get; set; }
        public string creatorserver { get; set; }
    }
}
