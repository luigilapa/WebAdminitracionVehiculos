using Modelo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5.Extencion
{
    public class OpcionRolExt
    {
        [Key]
        public int Id { get; set; }

        public int IdRol { get; set; }
        public ICollection<OpcionExt> Opciones { get; set; }
    }


    public class OpcionExt
    {
        public int Id { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public bool Bloqueo { get; set; }
    }
}