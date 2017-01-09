namespace Inspinia_MVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Worker
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(25)]
        public string Position { get; set; }

        [StringLength(150)]
        public string Office { get; set; }

        [StringLength(25)]
        public string Extn { get; set; }

        public int Salary { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public Nullable<System.DateTime> DateRegister { get; set; }
    }
}
