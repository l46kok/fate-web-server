﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class frsDb : DbContext
    {
        public frsDb()
            : base("name=frsDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<attributeinfo> attributeinfo { get; set; }
        public virtual DbSet<attributelearn> attributelearn { get; set; }
        public virtual DbSet<game> game { get; set; }
        public virtual DbSet<gameitempurchase> gameitempurchase { get; set; }
        public virtual DbSet<gameplayerdetail> gameplayerdetail { get; set; }
        public virtual DbSet<godshelpinfo> godshelpinfo { get; set; }
        public virtual DbSet<godshelpuse> godshelpuse { get; set; }
        public virtual DbSet<herostatinfo> herostatinfo { get; set; }
        public virtual DbSet<herostatlearn> herostatlearn { get; set; }
        public virtual DbSet<herotype> herotype { get; set; }
        public virtual DbSet<herotypename> herotypename { get; set; }
        public virtual DbSet<iteminfo> iteminfo { get; set; }
        public virtual DbSet<player> player { get; set; }
        public virtual DbSet<playerherostat> playerherostat { get; set; }
        public virtual DbSet<playerstat> playerstat { get; set; }
        public virtual DbSet<ranking> ranking { get; set; }
        public virtual DbSet<server> server { get; set; }
        public virtual DbSet<commandsealuse> commandsealuse { get; set; }
    }
}
