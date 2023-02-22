using System; 
using Microsoft.EntityFrameworkCore;
using api_autore_libros.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using encuestas_backend.Entidades;

namespace encuestas_backend
{
    public class AplicationDbContext : IdentityDbContext<UserCustom>
    {
		public AplicationDbContext( DbContextOptions options ) : base(options)
		{ }
		public DbSet<Categorias> Categorias { get; set; } 
		public DbSet<Cuestionario> Cuestionario { get; set; } 
		public DbSet<CuestionarioPreguntas> CuestionarioPreguntas { get; set; } 
		public DbSet<CuestionarioPreguntasRespuestas> CuestionarioPreguntasRespuestas { get; set; } 
    }
}