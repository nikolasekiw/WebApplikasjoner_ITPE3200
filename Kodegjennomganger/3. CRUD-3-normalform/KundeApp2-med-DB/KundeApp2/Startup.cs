using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KundeApp2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<KundeContext>(options =>
                            options.UseSqlite("Data Source=Kunde.db"));
        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /**
             * env: environment. 
             * Setningen under er kommentert ut. 
             * DBInit.Initialize(app). App kommer fra grensesnittet over (IApplica....)
             * Vi kunne godt ha gjort utenfor metoden if-setningen, men det betyr at hvis
             * vi switcher ofer til produksjonsmodus, s? kan man unng? ? initialisere 
             * databasen, mens hvis vi legger den utenfor if (DBInit.Ini...), s? 
             * vil vi kj?re den bestandig. Hvis vi kj?rer appen og f?r med DBInit..
             * s? f?r vi inn de initializerte kundene standard inn i appen. 
            **/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DBInit.Initialize(app); // denne m? fjernes dersom vi vil beholde dataene i databasen og ikke initialisere 
            }

            app.UseRouting();

            app.UseStaticFiles(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
