using System;
using System.ComponentModel.DataAnnotations;

namespace encuestas_backend.TDOs
{
	public class UsuarioRegistro
	{
		[Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string Password { get; set; }
	}
}

