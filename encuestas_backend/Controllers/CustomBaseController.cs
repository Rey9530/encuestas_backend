using AutoMapper;
using encuestas_backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace encuestas_backend.Controllers
{ 
    public class CustomBaseController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(AplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync();
            return  mapper.Map<List<TDTO>>(entidades);
        }


        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync( x => x.Id==id );
            if(entidades==null){
                return NotFound();
            }
            return  mapper.Map<TDTO>(entidades);
        }

        protected async Task<ActionResult<TLectura>> Post<TCreacion, TEntidad, TLectura>(TCreacion creacionDTO) where TEntidad : class
        { 
            var entidad = mapper.Map<TEntidad>(creacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            return mapper.Map<TLectura>(entidad); 
        }



        protected async Task<ActionResult<TLectura>> Put<TCreacion, TEntidad, TLectura>(TCreacion creacionDTO, int id) where TEntidad : class, IId
        { 
            var datos = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync( e => e.Id==id );
            if(datos==null){
                return NotFound();
            }
            var categoria = mapper.Map<TEntidad>(creacionDTO);
            categoria.Id=id;
            context.Entry(categoria).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return mapper.Map<TLectura>(categoria);
        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync( e => e.Id==id );
            if(entidad==null){
                return NotFound();
            }
            context.Remove(entidad);
            await context.SaveChangesAsync();
            return NoContent();
        }
 


    }
}