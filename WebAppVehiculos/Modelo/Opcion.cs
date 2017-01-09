namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Opcion")]
    public partial class Opcion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opcion()
        {
            OpcionRol = new HashSet<OpcionRol>();
        }

        [Key]
        public int idOpcion { get; set; }

        [StringLength(50)]
        public string modulo { get; set; }

        [StringLength(100)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string controlador { get; set; }

        [Required]
        [StringLength(50)]
        public string accion { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpcionRol> OpcionRol { get; set; }
    }
}
