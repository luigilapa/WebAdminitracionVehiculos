namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Parametros
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        [StringLength(2000)]
        public string valor_cadena_1 { get; set; }

        [StringLength(2000)]
        public string valor_cadena_2 { get; set; }

        public bool? activo { get; set; }
    }
}
