using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using encuestas_backend.TDOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace encuestas_backend.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthController( UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
		{
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

		[HttpPost("login")]
		public async Task<ActionResult<RespuestaAuth>> login(UsuarioRegistro usuarioRegistro)
		{
			var resultado = await signInManager.PasswordSignInAsync(usuarioRegistro.email, usuarioRegistro.Password, isPersistent:false, lockoutOnFailure:false);

			if (resultado.Succeeded)
			{
				return CostruirToken(usuarioRegistro);
			}
			else
            {
                return BadRequest("Login Incorrecto");
            }
		}




            [HttpPost("registrar")]
		public async Task<ActionResult<RespuestaAuth>> Registrar(UsuarioRegistro usuarioRegistro) {

			var usuario = new IdentityUser { UserName = usuarioRegistro.email, Email = usuarioRegistro.email };
			var resultado = await userManager.CreateAsync(usuario, usuarioRegistro.Password);
			if (resultado.Succeeded)
			{
				return CostruirToken(usuarioRegistro);

            }
			else {
				return BadRequest(resultado.Errors);
			}
		}

		private RespuestaAuth CostruirToken(UsuarioRegistro usuarioRegistro)
        {
            var expiration = DateTime.UtcNow.AddHours(8);
            var claims = new List<Claim>()
			{
				new Claim("email",usuarioRegistro.email),
                new Claim("expiration",expiration.ToString()),
            };
			var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
			var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

			var securityToken = new JwtSecurityToken(issuer:null, audience:null, claims:claims, signingCredentials:creds);

			return new RespuestaAuth()
			{
				token = new JwtSecurityTokenHandler().WriteToken(securityToken),
				Expiracion = expiration
			};

		}
	}
}

