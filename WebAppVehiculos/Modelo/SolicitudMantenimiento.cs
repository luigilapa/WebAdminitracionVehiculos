namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SolicitudMantenimiento")]
    public partial class SolicitudMantenimiento
    {
        [Key]
        public int IdSolicitud { get; set; }

        public int IdVehiculo { get; set; }

        public int IdChofer { get; set; }

        [Required]
        [StringLength(30)]
        public string TipoMantenimiento { get; set; }

        [StringLength(30)]
        public string SubTipoMantimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string Detalle { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaIngreso { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaEstimadaSalida { get; set; }

        public bool Aprobado { get; set; }

        public bool Desaprobado { get; set; }

        public bool Activo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }
    }
}
