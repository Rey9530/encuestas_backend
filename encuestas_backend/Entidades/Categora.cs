using System.ComponentModel.DataAnnotations;
using encuestas_backend.Enums;

namespace api_autore_libros.Entidades
{
    public class Categorias{
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public EstadosRegistro Estado { get; set; } = EstadosRegistro.Activo;
    }
}