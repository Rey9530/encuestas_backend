using api_autore_libros.Entidades;
using AutoMapper;
using encuestas_backend.Enums;
using encuestas_backend.TDOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuestas_backend.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriasController( AplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get(){
            var categorias = await context.Categorias.OrderBy(e=>e.Id).ToListAsync();
            return mapper.Map<List<CategoriaDTO>>(categorias);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetOne(int id){
            var categorias = await context.Categorias.Where( e=> e.Id==id ).OrderBy(e=>e.Id).FirstOrDefaultAsync();
            return mapper.Map<CategoriaDTO>(categorias);
        }
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> InsertOne([FromBody] CategoriaCrearDTO categoriaCrearDTO){
            var categoria = mapper.Map<Categorias>(categoriaCrearDTO);
            context.Add(categoria);
            await context.SaveChangesAsync();
            var categoriaResp = mapper.Map<CategoriaDTO>(categoria);
            return categoriaResp;
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> UpdatetOne(int id,[FromBody] CategoriaCrearDTO categoriaCrearDTO){

            var datos = await context.Categorias.FirstOrDefaultAsync( e => e.Id==id );
            if(datos==null){
                return NotFound();
            }
            var categoria = mapper.Map<Categorias>(categoriaCrearDTO);
            categoria.Id=id;
            context.Entry(categoria).State = EntityState.Modified;
            await context.SaveChangesAsync();
            var categoriaResp = mapper.Map<CategoriaDTO>(categoria);
            return categoriaResp;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DelteOne(int id){
            var categoria = await context.Categorias.FirstOrDefaultAsync( e => e.Id==id );
            if(categoria==null){
                return NotFound();
            }
            context.Remove(categoria);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}