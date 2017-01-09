namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistroActividadDetalle")]
    public partial class RegistroActividadDetalle
    {
        [Key]
        public int IdRegistroDetalle { get; set; }

        public int IdRegistro { get; set; }

        public TimeSpan Hora { get; set; }

        [Required]
        [StringLength(200)]
        public string LugarSalida { get; set; }

        [Required]
        [StringLength(200)]
        public string LugarLlegada { get; set; }

        [Required]
        [StringLength(300)]
        public string Motivo { get; set; }

        [StringLength(100)]
        public string JefeDepartamental { get; set; }

        public bool Activo { get; set; }

        public virtual RegistroActividad RegistroActividad { get; set; }
    }
}
