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
    public class Startup
    {
        /**
         * Nå skal vi se på hvordan bruke sessions for å holde rede på om en innlogging fungerer eller ikke. Altså om
         * en person er logget inn eller ikke, og unngå at man kan gå til sider dersom man ikke er logget inn. F.eks. gå direkte til
         * lagre.html url-en uten å være logget inn. For å få sessions til å fungere må man først i startup.cs legge til 
         * service.AddSession og med alle de opsjonene som er inni der. 
        **/
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<KundeContext>(options =>
                            options.UseSqlite("Data Source=Kunde.db"));
            services.AddScoped<IKundeRepository, KundeRepository>();
            // I tillegg til under må pakken Microsoft.AspNetCore.Session legges til i NuGet
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                //dette vil si at en sesjon vil være aktiv inntil 30 minutter, og hvis ikke brukeren gjør noe i applikasjonen i
                //løpet av 30 minutter så blir man automatisk logget ut. 
                options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutter
                options.Cookie.IsEssential = true;
            });
            // Denne må også være med:
            services.AddDistributedMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddFile("Logs/KundeLog.txt");
                DBInit.Initialize(app); // denne m? fjernes dersom vi vil beholde dataene i databasen og ikke initialisere 
            }

            app.UseRouting();

            // UseSession!
            app.UseSession();

            //app.UseAuthentication();

            app.UseStaticFiles(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
