namespace encuestas_backend.Entidades
{
    public class CuestionarioPreguntasRespuestas: IId
    {
        public int Id { get; set; }
        public string Respuesta { get; set; }
        public bool EsCorrecta { get; set; } = false; 
        public CuestionarioPreguntas Pregunta { get; set; }

    }
}