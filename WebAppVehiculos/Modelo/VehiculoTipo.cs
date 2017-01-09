namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehiculoTipo")]
    public partial class VehiculoTipo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehiculoTipo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        [Key]
        public int idTipo { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(100)]
        public string descripcion { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
