using Microsoft.EntityFrameworkCore;
using MinimalApiPeliculas.EntityFramework;
using MinimalApiPeliculas.Repositorio;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using FluentValidation;
using System.Reflection;

namespace MinimalApiPeliculas
{
    public static class Program_Services
    {

        public static void Configure(WebApplicationBuilder builder)
        {

            //json, configuracion de ignore cycles
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            //cors, declaracion de politicas
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            //cache para las apis
            builder.Services.AddOutputCache();


            #region EF

            //entity framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(defaultConnection);
            });

           

            #endregion



            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new() { Title = "Api Henry Alberto Chavez Chavez", Version = "v1", Description = "Api Crud de Persona" });
                });
            }





            //repositorios
            builder.Services.AddScoped<IPersonaRepository, PersonaRepository>(); 



       


            //fluent validation
            //   builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());



            //mediator 
            builder.Services.AddMediatR(config =>
                  {
                      config.RegisterServicesFromAssemblies(typeof(Program).Assembly);

                      config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                  });
 


            //automapper
            builder.Services.AddAutoMapper(typeof(Program));




             

        }
    }
}
