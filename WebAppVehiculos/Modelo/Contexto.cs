namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }

        public virtual DbSet<Opcion> Opcion { get; set; }
        public virtual DbSet<OpcionRol> OpcionRol { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<RegistroActividad> RegistroActividad { get; set; }
        public virtual DbSet<RegistroActividadDetalle> RegistroActividadDetalle { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<SolicitudMantenimiento> SolicitudMantenimiento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<VehiculoChofer> VehiculoChofer { get; set; }
        public virtual DbSet<VehiculoMarca> VehiculoMarca { get; set; }
        public virtual DbSet<VehiculoTipo> VehiculoTipo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Opcion>()
                .Property(e => e.modulo)
                .IsUnicode(false);

            modelBuilder.Entity<Opcion>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Opcion>()
                .Property(e => e.controlador)
                .IsUnicode(false);

            modelBuilder.Entity<Opcion>()
                .Property(e => e.accion)
                .IsUnicode(false);

            modelBuilder.Entity<Opcion>()
                .HasMany(e => e.OpcionRol)
                .WithRequired(e => e.Opcion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.codigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.valor_cadena_1)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.valor_cadena_2)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroActividad>()
                .HasMany(e => e.RegistroActividadDetalle)
                .WithRequired(e => e.RegistroActividad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegistroActividadDetalle>()
                .Property(e => e.LugarSalida)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroActividadDetalle>()
                .Property(e => e.LugarLlegada)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroActividadDetalle>()
                .Property(e => e.Motivo)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroActividadDetalle>()
                .Property(e => e.JefeDepartamental)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.NombreRol)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.OpcionRol)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SolicitudMantenimiento>()
                .Property(e => e.TipoMantenimiento)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudMantenimiento>()
                .Property(e => e.SubTipoMantimiento)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudMantenimiento>()
                .Property(e => e.Detalle)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.TipoLicencia)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.RutaFoto)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreFoto)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.RegistroActividad)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdChofer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.SolicitudMantenimiento)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdChofer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.VehiculoChofer)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdChofer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.placaLetras)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.placaNumeros)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.color1)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.color2)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(e => e.RegistroActividad)
                .WithRequired(e => e.Vehiculo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(e => e.SolicitudMantenimiento)
                .WithRequired(e => e.Vehiculo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(e => e.VehiculoChofer)
                .WithRequired(e => e.Vehiculo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehiculoMarca>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculoMarca>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculoMarca>()
                .HasMany(e => e.Vehiculo)
                .WithRequired(e => e.VehiculoMarca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehiculoTipo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculoTipo>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<VehiculoTipo>()
                .HasMany(e => e.Vehiculo)
                .WithRequired(e => e.VehiculoTipo)
                .WillCascadeOnDelete(false);
        }
    }
}
