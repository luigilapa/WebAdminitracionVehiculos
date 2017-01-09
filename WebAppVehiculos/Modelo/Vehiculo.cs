namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehiculo")]
    public partial class Vehiculo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculo()
        {
            RegistroActividad = new HashSet<RegistroActividad>();
            SolicitudMantenimiento = new HashSet<SolicitudMantenimiento>();
            VehiculoChofer = new HashSet<VehiculoChofer>();
        }

        [Key]
        public int idVehiculo { get; set; }

        [Required]
        [StringLength(3)]
        public string placaLetras { get; set; }

        [Required]
        [StringLength(4)]
        public string placaNumeros { get; set; }

        public int idMarca { get; set; }

        public int idTipo { get; set; }

        [Required]
        [StringLength(50)]
        public string modelo { get; set; }

        public int? anio { get; set; }

        [StringLength(15)]
        public string color1 { get; set; }

        [StringLength(15)]
        public string color2 { get; set; }

        public int? idResponsable { get; set; }

        public bool activo { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroActividad> RegistroActividad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudMantenimiento> SolicitudMantenimiento { get; set; }

        public virtual VehiculoMarca VehiculoMarca { get; set; }

        public virtual VehiculoTipo VehiculoTipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehiculoChofer> VehiculoChofer { get; set; }
    }
}
