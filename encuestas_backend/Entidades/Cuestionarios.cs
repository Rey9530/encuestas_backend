using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using encuestas_backend.Enums;

namespace encuestas_backend.Entidades
{
    public class Cuestionario: IId
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tene mas de {1} caracter")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [StringLength(maximumLength: 500, ErrorMessage = "El campo {0} no debe tene mas de {1} caracter")]
        public string Descripcion { get; set; } 
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public EstadosRegistro Estado { get; set; } = EstadosRegistro.Activo;
        public string idUsuarioCreador { get; set; } 
        public List<UserCustom> Usuario { get; set; }
    }
}