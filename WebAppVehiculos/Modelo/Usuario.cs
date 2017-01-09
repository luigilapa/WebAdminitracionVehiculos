namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            RegistroActividad = new HashSet<RegistroActividad>();
            SolicitudMantenimiento = new HashSet<SolicitudMantenimiento>();
            VehiculoChofer = new HashSet<VehiculoChofer>();
        }

        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }

        public int? IdRol { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        public int? Telefono { get; set; }

        public string Clave { get; set; }

        public bool Activo { get; set; }

        [StringLength(20)]
        public string NombreUsuario { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        public bool EsChofer { get; set; }

        [StringLength(50)]
        public string TipoLicencia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaVencimientoLicencia { get; set; }

        [StringLength(300)]
        public string RutaFoto { get; set; }

        [StringLength(50)]
        public string NombreFoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroActividad> RegistroActividad { get; set; }

        public virtual Rol Rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudMantenimiento> SolicitudMantenimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehiculoChofer> VehiculoChofer { get; set; }
    }
}
