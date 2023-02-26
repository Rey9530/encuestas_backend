using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using encuestas_backend.Entidades;
using encuestas_backend.TDOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace encuestas_backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserCustom> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<UserCustom> signInManager;

        public AuthController(UserManager<UserCustom> userManager, IConfiguration configuration, SignInManager<UserCustom> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RespuestaHttp<RespuestaAuth>>> login(UsuarioIniarSesion usuarioRegistro)
        {
            try
            {
                var resultado = await signInManager.PasswordSignInAsync(usuarioRegistro.email, usuarioRegistro.Password, isPersistent: false, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    var usuario = await userManager.FindByEmailAsync(usuarioRegistro.email);
                    var usuarioCrearToken = new UsuarioRegistro()
                    {
                        email = usuarioRegistro.email
                    };
                    // return CostruirToken(usuarioCrearToken, usuario.UserName);
                    return new RespuestaHttp<RespuestaAuth>()
                    {
                        success = true,
                        msg = "Usuario Creado",
                        data = CostruirToken(usuarioCrearToken, usuario.UserName)
                    };
                }
                else
                {
                    return Unauthorized( );
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }




        [HttpPost("registrar")] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RespuestaHttp<RespuestaAuth>>> Registrar([FromBody] UsuarioRegistro usuarioRegistro)
        {
            var usuarioExiste = await userManager.FindByEmailAsync(usuarioRegistro.email);
            if (usuarioExiste != null)
            {
                return BadRequest(new RespuestaHttp<dynamic>()
                {
                    success = false,
                    msg = "El usuario ya existe",
                    data = null
                });
            }

            var usuario = new UserCustom { UserName = usuarioRegistro.usuario, Email = usuarioRegistro.email };
            var resultado = await userManager.CreateAsync(usuario, usuarioRegistro.Password);
            if (resultado.Succeeded)
            {
                return new RespuestaHttp<RespuestaAuth>()
                {
                    success = true,
                    msg = "Usuario Creado",
                    data = CostruirToken(usuarioRegistro, usuarioRegistro.usuario)
                };
            }
            else
            {
                return BadRequest(new RespuestaHttp<dynamic>()
                {
                    success = false,
                    msg = "Error al Crear el usuario",
                    data = resultado.Errors
                });
            }
        }

        [HttpGet("renovartoken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAuth>> RenovarToke()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var usuario = await userManager.FindByEmailAsync(emailClaim.Value);
            var usuarioRegistro = new UsuarioRegistro()
            {
                email = emailClaim.Value
            };
            return CostruirToken(usuarioRegistro, usuario.UserName);
        }
        private RespuestaAuth CostruirToken(UsuarioRegistro usuarioRegistro, string usuario)
        {
            var expiration = DateTime.UtcNow.AddHours(8);
            var claims = new List<Claim>()
            {
                new Claim("email",usuarioRegistro.email),
                new Claim("expiration",expiration.ToString()),
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, signingCredentials: creds, expires: expiration);

            return new RespuestaAuth()
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration,
                correo = usuarioRegistro.email,
                usuario = usuario
            };

        }
    }
}

