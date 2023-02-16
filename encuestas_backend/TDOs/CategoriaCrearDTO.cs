using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace encuestas_backend.TDOs
{
    public class CategoriaCrearDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}