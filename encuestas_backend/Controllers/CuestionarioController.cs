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
            cuestionario.Usuario = usuario;
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

        [HttpPost("agregar_pregunta/{id:int}")]
        public async Task<ActionResult<CuestionarioPreguntaDTO>> AgregarPregunta(int id,[FromBody] PreguntaCreacionDTO  preguntaDTO){
            var resp = context.Cuestionario.Where(x => x.Id==id ).FirstOrDefault();
            if(resp==null){
                return NotFound();
            }
            var pregunta = mapper.Map<CuestionarioPreguntas>(preguntaDTO);
            pregunta.Cuestionario = resp;
            context.Add(pregunta);
            await context.SaveChangesAsync();
            return mapper.Map<CuestionarioPreguntaDTO>(pregunta);
        }


        [HttpGet("obtener_pregunta/{id:int}")]
        public async Task<ActionResult<CuestionarioPreguntaDTO>> GetPregunta(int id){
            var resp = await context.CuestionarioPreguntas.Include(e => e.Respuestas ).Where(x => x.Id==id ).AsNoTracking().FirstOrDefaultAsync();
            if(resp==null){
                return NotFound();
            }
            return mapper.Map<CuestionarioPreguntaDTO>(resp);
        }



        [HttpPut("editar_pregunta/{id:int}")]
        public async Task<ActionResult<CuestionarioPreguntaDTO>> EditPregunta(int id,[FromBody] PreguntaCreacionDTO  preguntaDTO){ 
            return await Put<PreguntaCreacionDTO, CuestionarioPreguntas, CuestionarioPreguntaDTO>(preguntaDTO, id);  
        }

        [HttpDelete("eliminar_pregunta/{id:int}")]
        public async Task<ActionResult> DelteOneQuestion(int id){
            
            var entidad = await context.CuestionarioPreguntas.Include(e => e.Respuestas).Where( e => e.Id==id ).FirstOrDefaultAsync();
            if(entidad==null){
                return NotFound();
            }
            context.RemoveRange(entidad.Respuestas);
            context.Remove(entidad);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("agregar_respuestas/{id:int}")]
        public async Task<ActionResult<RespuestasDTO>> AgregarRespuestas(int id,[FromBody] RespuestasCreacionDTO  respuestaDTO){
            var resp = context.CuestionarioPreguntas.Where(x => x.Id==id ).FirstOrDefault();
            if(resp==null){
                return NotFound();
            }
            var respuesta = mapper.Map<CuestionarioPreguntasRespuestas>(respuestaDTO);
            respuesta.Pregunta = resp;
            context.Add(respuesta);
            await context.SaveChangesAsync();
            return mapper.Map<RespuestasDTO>(respuesta);
        }
        [HttpDelete("eliminar_respuesta/{id:int}")]
        public async Task<ActionResult> DelteOneAnswer(int id){ 
            return await Delete<CuestionarioPreguntasRespuestas>(id); 
        }
    }
}