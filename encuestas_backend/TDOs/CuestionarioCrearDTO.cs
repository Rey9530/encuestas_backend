using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace encuestas_backend.TDOs
{
    public class CuestionarioCrearDTO
    { 
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tene mas de {1} caracter")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [StringLength(maximumLength: 500, ErrorMessage = "El campo {0} no debe tene mas de {1} caracter")]
        public string Descripcion { get; set; }  
    }
}