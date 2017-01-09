using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class WorkerMetadata
    {
        public int ID { get; set; }

        [Required(ErrorMessage="El nombre debe ser ingresado por....")]
        [DisplayName("Nombre")]

        public string Name { get; set; }

      [DisplayName("Cargo")]
        public string Position { get; set; }


         [DisplayName("Telf. Ofic.")]
         
        public string Office { get; set; }

       [DisplayName("Extension")]
        public string Extn { get; set; }

         [DisplayName("Sueldo")]
        [Range(100, 1000)]
        public int Salary { get; set; }

        [Required]
        //[EmailAddress]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Correo Elec.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
          [DisplayName("Fecha ingreso")]
        public DateTime DateRegister { get; set; }


    }


    [MetadataType(typeof(WorkerMetadata))]
    public partial class Worker
    {
       
    }

}