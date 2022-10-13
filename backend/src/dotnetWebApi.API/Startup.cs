using dotnetWebApi.Application;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Persistence;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System;

namespace dotnetWebApi.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<dotnetWebAPIContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            // remove o loop devido a auto referencia
            services.AddControllers().AddNewtonsoftJson(
                x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Injetando dependências 
            services.AddScoped<IGeralPersist, GeralPersist>();
            
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventPersist, EventPersist>();

            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<ILotePersist, LotePersist>();

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetWebApi.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnetWebApi.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(access => access.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
