using encuestas_backend.Entidades;

namespace encuestas_backend.TDOs
{
    public class CuestionarioPreguntaDTO
    {
        
        public int Id { get; set; }
        public string Pregunta { get; set; } 
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;  
        public List<RespuestasDTO> Respuestas { get; set; }
    }
}