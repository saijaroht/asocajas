﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asocajas
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AsocajasBDEntities : DbContext
    {
        public AsocajasBDEntities()
            : base("name=AsocajasBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<LTLogApp> LTLogApp { get; set; }
        public DbSet<LTLogConsultasAni> LTLogConsultasAni { get; set; }
        public DbSet<LTLogEventos> LTLogEventos { get; set; }
        public DbSet<RCCF> RCCF { get; set; }
        public DbSet<RRole> RRole { get; set; }
        public DbSet<RRptaAsocajas> RRptaAsocajas { get; set; }
        public DbSet<RRptaRnec> RRptaRnec { get; set; }
        public DbSet<RUsuario> RUsuario { get; set; }
    }
}
