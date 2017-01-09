namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistroActividad")]
    public partial class RegistroActividad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistroActividad()
        {
            RegistroActividadDetalle = new HashSet<RegistroActividadDetalle>();
        }

        [Key]
        public int IdRegistro { get; set; }

        public int IdChofer { get; set; }

        public int Idvehiculo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        public decimal KmInicial { get; set; }

        public decimal KmFinal { get; set; }

        public bool Activo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroActividadDetalle> RegistroActividadDetalle { get; set; }
    }
}
