using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using encuestas_backend.Enums;

namespace encuestas_backend.TDOs
{
    public class CategoriaDTO
    {
        
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public EstadosRegistro Estado { get; set; }
    }
}