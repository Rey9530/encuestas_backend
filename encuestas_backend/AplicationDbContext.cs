using System; 
using Microsoft.EntityFrameworkCore;
using api_autore_libros.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace encuestas_backend
{
    public class AplicationDbContext : IdentityDbContext
    {
		public AplicationDbContext( DbContextOptions options ) : base(options)
		{
		}
		public DbSet<Categorias> Categorias { get; set; }
    }
}