using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KundeApp2
{
    /**
     * For å få til logging må vi legge inn en pakke som heter: Serilot.Excentions.Logging.File
    **/
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<KundeContext>(options =>
                            options.UseSqlite("Data Source=Kunde.db"));
            services.AddScoped<IKundeRepository, KundeRepository>();
        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Må spesifisere hvilke fil vi skal bruke. .AddFile tar inn en streng med filnavnet. Logger må initieres i kunde-controlleren
                loggerFactory.AddFile("Logs/KundeLog.txt");
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
