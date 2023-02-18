using AutoMapper;
using encuestas_backend.Entidades;
using encuestas_backend.TDOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuestas_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuestionarioController : CustomBaseController
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        public UserManager<UserCustom> userManager { get; }

        public CuestionarioController(AplicationDbContext context, IMapper mapper, UserManager<UserCustom> userManager) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<CuestionarioDTO>>> GetAll(){ 
            return await Get<Cuestionario, CuestionarioDTO>(); 
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CuestionarioDTO>> GetOne(int id){ 
            return await Get<Cuestionario, CuestionarioDTO>(id);  
        }
        
        [HttpPost]
        public async Task<ActionResult<CuestionarioDTO>> InsertOne([FromBody] CuestionarioCrearDTO cuestionarioCrearDTO){ 
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type=="email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var cuestionario = mapper.Map<Cuestionario>(cuestionarioCrearDTO);
            cuestionario.idUsuarioCreador = usuario.Id;
            cuestionario.FechaCreacion = DateTime.Now;
            context.Add(cuestionario);
            await context.SaveChangesAsync();
            return mapper.Map<CuestionarioDTO>(cuestionario);
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<CuestionarioDTO>> UpdateOne(int id,[FromBody] CuestionarioCrearDTO cuestionarioCrearDTO){  
            return await Put<CuestionarioCrearDTO, Cuestionario, CuestionarioDTO>(cuestionarioCrearDTO, id); 
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DelteOne(int id){
            return await Delete<Cuestionario>(id);
        }
    }
}