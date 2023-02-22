using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace encuestas_backend.TDOs
{
    public class RespuestasCreacionDTO
    {
        public string Respuesta { get; set; }
        public bool EsCorrecta { get; set; } = false;
    }
}