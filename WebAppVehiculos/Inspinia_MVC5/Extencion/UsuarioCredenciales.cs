using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5.Extencion
{
    public class UsuarioCredenciales
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Usuario { get; set; }

        [Required]
        public string Clave { get; set; }

        public string NuevoUsuario { get; set; }

        public string NuevaClave { get; set; }

        [Compare("NuevaClave")]
        public string ConfirmarClave { get; set; }
        
    }
}