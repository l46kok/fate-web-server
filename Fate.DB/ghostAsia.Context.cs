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
    
    public partial class ghostEntities : DbContext
    {
        public ghostEntities()
            : base("name=ghostEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admins> admins { get; set; }
        public virtual DbSet<bans> bans { get; set; }
        public virtual DbSet<dotagames> dotagames { get; set; }
        public virtual DbSet<dotaplayers> dotaplayers { get; set; }
        public virtual DbSet<downloads> downloads { get; set; }
        public virtual DbSet<gameplayers> gameplayers { get; set; }
        public virtual DbSet<games> games { get; set; }
        public virtual DbSet<kicks> kicks { get; set; }
        public virtual DbSet<phrases> phrases { get; set; }
        public virtual DbSet<plugindb> plugindb { get; set; }
        public virtual DbSet<scores> scores { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<w3mmdplayers> w3mmdplayers { get; set; }
        public virtual DbSet<w3mmdvars> w3mmdvars { get; set; }
    }
}