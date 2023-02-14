using System.ComponentModel.DataAnnotations;

namespace api_autore_libros.Entidades
{
    public class Categorias{
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}