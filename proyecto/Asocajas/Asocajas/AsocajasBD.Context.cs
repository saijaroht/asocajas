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
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
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
    
        public DbSet<RUsuario> RUsuario { get; set; }
        public DbSet<LTLogApp> LTLogApp { get; set; }
        public DbSet<LTLogConsultasAni> LTLogConsultasAni { get; set; }
        public DbSet<RCCF> RCCF { get; set; }
        public DbSet<RDatoBasico> RDatoBasico { get; set; }
        public DbSet<RMenu> RMenu { get; set; }
        public DbSet<RRole> RRole { get; set; }
        public DbSet<RRptaAsocajas> RRptaAsocajas { get; set; }
        public DbSet<RRptaRnec> RRptaRnec { get; set; }
        public DbSet<RTipoDatoBasico> RTipoDatoBasico { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<LTLogEventos> LTLogEventos { get; set; }
    
    }
}
