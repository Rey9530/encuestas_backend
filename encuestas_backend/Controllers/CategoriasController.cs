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
    public class CategoriasController : CustomBaseController
    { 

        public CategoriasController( AplicationDbContext context, IMapper mapper): base(context, mapper)
        {    }
        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get(){
            return await Get<Categorias, CategoriaDTO>();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetOne(int id){
            return await Get<Categorias, CategoriaDTO>(id);
        }
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> InsertOne([FromBody] CategoriaCrearDTO categoriaCrearDTO){
            return await Post<CategoriaCrearDTO, Categorias, CategoriaDTO>(categoriaCrearDTO); 
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> UpdatetOne(int id,[FromBody] CategoriaCrearDTO categoriaCrearDTO){
            return await Put<CategoriaCrearDTO, Categorias, CategoriaDTO>(categoriaCrearDTO, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DelteOne(int id){
            return await Delete<Categorias>(id);
        }
    }
}