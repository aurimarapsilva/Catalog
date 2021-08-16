using System;
using System.IO;
using System.Reflection;
using catalog.infra.DataContext;
using catalog.infra.Repositories;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace catalog.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddResponseCompression();

            // Configura injeção de dependencia do banco de dados na aplicação
            services.AddDbContext<StoreDataContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("conn"))
            );

            //// Configura as dependencias dos manipuladores
            //services.AddTransient<HandlerCatalogBrand, HandlerCatalogBrand>();
            //services.AddTransient<HandlerCatalogItem, HandlerCatalogItem>();
            //services.AddTransient<HandlerCatalogType, HandlerCatalogType>();

            //// Configura as dependencias dos repositorios
            //services.AddTransient<ICatalogBrandRepository, CatalogBrandRepository>();
            //services.AddTransient<ICatalogItemRepository, CatalogItemRepository>();
            //services.AddTransient<ICatalogTypeRepository, CatalogTypeRepository>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Catalog Service",
                    Description = "Service responsible for product registration and inventory control",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor Hernandes",
                        Email = "programacao.hernandes@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/victor-hernandes-57412562/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT license",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Catalog.Core.xml"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service v1");
                x.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
