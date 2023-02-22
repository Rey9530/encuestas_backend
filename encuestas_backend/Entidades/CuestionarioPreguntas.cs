namespace encuestas_backend.Entidades
{
    public class CuestionarioPreguntas : IId
    {
        public int Id { get; set; }
        public string Pregunta { get; set; } 
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow; 
        public Cuestionario Cuestionario { get; set; }
        public List<CuestionarioPreguntasRespuestas> Respuestas { get; set; }
    }
}