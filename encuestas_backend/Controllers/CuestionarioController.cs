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
    public class CuestionarioController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public UserManager<UserCustom> userManager { get; }

        public CuestionarioController(AplicationDbContext context, IMapper mapper, UserManager<UserCustom> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<CuestionarioDTO>>> GetAll(){
            var cuestionarios = await context.Cuestionario.OrderBy( e=> e.IdCuestionario ).ToListAsync();
            return mapper.Map<List<CuestionarioDTO>>(cuestionarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CuestionarioDTO>> GetOne(int id){
            var cuestionario = await context.Cuestionario.FirstOrDefaultAsync(e => e.IdCuestionario==id);
            return mapper.Map<CuestionarioDTO>(cuestionario);
        }
// UserManager
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
            var datos = await context.Cuestionario.FirstOrDefaultAsync( e => e.IdCuestionario==id );
            if(datos==null){
                return NotFound();
            }
            var cuestionario = mapper.Map<Cuestionario>(cuestionarioCrearDTO); 
            cuestionario.IdCuestionario=id;
            context.Entry(cuestionario).State = EntityState.Modified; 
            await context.SaveChangesAsync();
            return mapper.Map<CuestionarioDTO>(cuestionario);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DelteOne(int id){
            var entity = await context.Cuestionario.FirstOrDefaultAsync( e => e.IdCuestionario==id );
            if(entity==null){
                return NotFound();
            }
            context.Remove(entity);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}