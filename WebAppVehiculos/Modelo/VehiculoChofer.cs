namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehiculoChofer")]
    public partial class VehiculoChofer
    {
        [Key]
        public int IdVehiculoChofer { get; set; }

        public int IdChofer { get; set; }

        public int IdVehiculo { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public DateTime? FechaDesasignacion { get; set; }

        public bool Activo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Vehiculo Vehiculo { get; set; }
    }
}
