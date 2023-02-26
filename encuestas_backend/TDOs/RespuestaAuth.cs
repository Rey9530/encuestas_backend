using System;
namespace encuestas_backend.TDOs
{
	public class RespuestaAuth
	{
		public string token { get; set; }
		public string correo { get; set; }
		public string usuario { get; set; }
		public DateTime Expiracion { get; set; }
	}
}

