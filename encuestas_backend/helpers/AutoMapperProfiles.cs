using api_autore_libros.Entidades;
using AutoMapper;
using encuestas_backend.Entidades;
using encuestas_backend.TDOs;

namespace encuestas_backend.helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Categorias, CategoriaDTO>().ReverseMap();
            CreateMap<CategoriaCrearDTO, Categorias>();


            CreateMap<Cuestionario, CuestionarioDTO>().ReverseMap();
            CreateMap<CuestionarioCrearDTO, Cuestionario>();
        }
    }
}