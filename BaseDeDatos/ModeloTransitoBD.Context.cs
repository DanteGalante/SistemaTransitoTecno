﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseDeDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDTransitoEntities : DbContext
    {
        public BDTransitoEntities()
            : base("name=BDTransitoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Conductor> Conductores { get; set; }
        public virtual DbSet<Dictamen> Dictamenes { get; set; }
        public virtual DbSet<Reporte> Reportes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<DelegacionMunicipal> DelegacionesMunicipales { get; set; }
        public virtual DbSet<Fotografia> Fotografias { get; set; }
    }
}
