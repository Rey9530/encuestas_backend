using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using encuestas_backend.Entidades;
using encuestas_backend.Enums;

namespace encuestas_backend.TDOs
{
    public class CuestionarioDTO
    {
 
        public int Id { get; set; } 
        public string Titulo { get; set; } 
        public string Descripcion { get; set; }  
        public DateTime FechaCreacion { get; set; }
        public EstadosRegistro Estado { get; set; }  
        
    }
}