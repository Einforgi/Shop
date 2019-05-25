using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Web.Data;

namespace Shop.Web
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
            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            // La siguiente línea es para que nos injecte datos automáticamente mediante la clase
            // SeedDb que hemos creado para ese propósito
            services.AddTransient<SeedDb>();

            // La siguiente línea es para injectar la interface "IRepositoy" pero con la
            // implementación de la clase "Repositoy.cs"
            services.AddScoped<IRepository, Repository>();

            // Nota: Con las dos líneas de código anteriores, hemos visto dos maneras
            // de injectar información en la base de datos local; el "AddTransient" tiene
            // un ciclo de vida más corto que el "AddScoped", es decir, se usa una vez y se destruye.
            // Cuando ejecutamos la aplicación, el AddTransient<SeedDb> llama al "seeder" para injectar
            // información en la base de datos en caso que no tuviese datos, realizando la injección
            // de la clase "SeedDb" y, una vez hecho eso, no queda la injección viva, sino que actua
            // acto seguido el "recolector de basura"; mientras que con la acción "AddScoped",
            // la injección queda permanente durante toda la ejecución de la aplicación, para que sea
            // reusada las veces que sean necesarias.

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
