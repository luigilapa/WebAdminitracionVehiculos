namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpcionRol")]
    public partial class OpcionRol
    {
        [Key]
        public int IdOpcionRol { get; set; }

        public int IdRol { get; set; }

        public int IdOpcion { get; set; }

        public bool TienePermiso { get; set; }

        public bool Activo { get; set; }

        public virtual Opcion Opcion { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
