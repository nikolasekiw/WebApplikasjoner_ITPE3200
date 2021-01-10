using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KundeOrdre
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
            /**
             * Vi m? i tillegg legge til -AddNewtonsoftJson, det er en pakke som gj?r
             * at komplekse strukturer som vi har n? blir konvertert til JSON, den innebygde
             * pakka i .NET core, for ? konvertere JSON fram og tilbake mellom klient og tjener, 
             * den har noen begrensninger. Denne bakken skal v?re med ? serialisere JSON strukturen, og
             * da m? Microsoft.AspNetCore.NewtonsoftJson installeres som pakke, hvis jeg ikke gj?r det
             * for jeg en feilmelding om at jeg har for mange niv?er. Det betyr at programvaren
             * i Asp.NETcore ikke h?ndterer denne relativt komplekse strukturen fordi i 
             * kundecontrolleren, s? har vi List av Kunde (List<Kunde>), og den er ganske kompleks n?, den
             * skal serialiseres, alts? legges p? et JSON format, og den JSON formatereren som 
             * ligger i standard .NETcore, den holder ikke helt. 
             * 
             * Til slutt: se i consollen hvordan denne kommer ut i JSON format (i inspiser) --> g?r p? network og preview
            **/

            //i utgangspunktet stod det bare .AddControllers() her og ikke noe mer. Det er greit ? bruke denne pakka uansett for da sikrer man at man ikke f?r noen problemer. 
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    // m? v?re med n?r det skal serialiseres "kompliserte" strukturer til JSON. 
                    // i tillegg m? Microsoft.AspNetCore.NewtonsoftJson installeres som pakke
        );
            services.AddDbContext<DB>(options => options.UseSqlite("Data Source=Kunde.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DBInit.init(app);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
