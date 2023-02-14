using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace encuestas_backend
{
	public class StartUp
	{
		public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigurationServices(IServiceCollection service)
        {

            // service.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            service.AddControllers();

            service.AddDbContext<AplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("defaultConnection")));
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( opciones => opciones.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey= new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["llavejwt"])
                        ),
                    ClockSkew = TimeSpan.Zero
                }); 
             //service.AddAuthentication();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            service.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();
            // service.AddAutoMapper(typeof(StartUp));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}