using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Business;
using WebApi.Data;
using WebApi.Models;
using WebApi.Controllers;

namespace WebApi
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
            // Configuration de la base de données TodoDbContext
            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuration de la base de données BoardDbContext
            services.AddDbContext<BoardDbContext>(options =>
                options.UseInMemoryDatabase("BoardDatabase"));

            // Configuration de la base de données TagDbContext
            services.AddDbContext<TagDbContext>(options =>
                options.UseInMemoryDatabase("TagDatabase"));

            // Configuration de la base de données fixe pour les Tags
            services.AddScoped<ITagRepository, DatabaseTagRepository>();

            // Configuration de la base de données fixe pour les Todos
            services.AddScoped<ITodoRepository, DatabaseTodoRepository>();

            services.AddControllers();

            // Configuration de Swagger
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Swagger uniquement dans l'environnement de développement
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
