using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebapplikasjonerOppgave1.DAL;
using WebapplikasjonerOppgave1.Models;
using static WebapplikasjonerOppgave1.Models.NorwayContext;

namespace WebapplikasjonerOppgave1.Models
{
    public static class DbInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<NorwayContext>();

               // må slette og opprette databasen hver gang når den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                /*---------OPPRETTER STASJONER--------*/
                var stasjon1 = new Stasjon { StasjonsNavn = "Oslo" };
                var stasjon2 = new Stasjon { StasjonsNavn = "Bergen" };
                var stasjon3 = new Stasjon { StasjonsNavn = "Trondheim" };
                var stasjon4 = new Stasjon { StasjonsNavn = "Bodø" };

                context.Stasjoner.Add(stasjon1);
                context.Stasjoner.Add(stasjon2);
                context.Stasjoner.Add(stasjon3);
                context.Stasjoner.Add(stasjon4);


                /*---------OPPRETTER DATOER--------*/
                string dato1 = "12/12/2020";
                string dato2 = "24/12/2020";
                string dato3 = "01/01/2021";

                /*---------OPPRETTER TIDER--------*/
                string tid1 = "09:00";
                string tid2 = "15:00";
                string tid3 = "20:00";


                /*---------OPPRETTER TURER--------*/
                var tur1 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur2 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur3 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur4 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur5 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur6 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur7 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur8 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur9 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon2, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur10 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur11 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur12 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur13 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur14 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur15 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur16 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur17 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur18 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon3, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur19 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur20 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur21 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur22 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur23 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur24 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur25 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur26 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur27 = new Turer { StartStasjon = stasjon1, EndeStasjon = stasjon4, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };

                var tur28 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur29 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur30 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur31 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur32 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur33 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur34 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur35 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur36 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon1, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur37 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur38 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur39 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur40 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur41 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur42 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur43 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur44 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur45 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon3, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur46 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur47 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur48 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur49 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur50 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur51 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur52 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur53 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur54 = new Turer { StartStasjon = stasjon2, EndeStasjon = stasjon4, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };

                var tur55 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur56 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur57 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur58 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur59 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur60 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur61 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur62 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur63 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon1, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur64 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur65 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur66 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur67 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur68 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur69 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur70 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur71 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur72 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon2, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur73 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur74 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur75 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur76 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur77 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur78 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur79 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur80 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur81 = new Turer { StartStasjon = stasjon3, EndeStasjon = stasjon4, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };

                var tur82 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur83 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur84 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur85 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur86 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur87 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur88 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur89 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur90 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon1, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur91 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur92 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur93 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur94 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur95 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid2, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur96 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur97 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur98 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur99 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon2, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur100 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid1, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur101 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid2, Dato = dato1, BarnePris = 50, VoksenPris = 100 };
                var tur102 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid3, Dato = dato1, BarnePris = 70, VoksenPris = 200 };
                var tur103 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid1, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur104 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur105 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid3, Dato = dato2, BarnePris = 70, VoksenPris = 200 };
                var tur106 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid1, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur107 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid2, Dato = dato3, BarnePris = 70, VoksenPris = 200 };
                var tur108 = new Turer { StartStasjon = stasjon4, EndeStasjon = stasjon3, Tid = tid3, Dato = dato3, BarnePris = 70, VoksenPris = 200 };


                context.Turer.Add(tur1);
                context.Turer.Add(tur2);
                context.Turer.Add(tur3);
                context.Turer.Add(tur4);
                context.Turer.Add(tur5);
                context.Turer.Add(tur6);
                context.Turer.Add(tur7);
                context.Turer.Add(tur8);
                context.Turer.Add(tur9);
                context.Turer.Add(tur10);
                context.Turer.Add(tur11);
                context.Turer.Add(tur12);
                context.Turer.Add(tur13);
                context.Turer.Add(tur14);
                context.Turer.Add(tur15);
                context.Turer.Add(tur16);
                context.Turer.Add(tur17);
                context.Turer.Add(tur18);
                context.Turer.Add(tur19);
                context.Turer.Add(tur20);
                context.Turer.Add(tur21);
                context.Turer.Add(tur22);
                context.Turer.Add(tur23);
                context.Turer.Add(tur24);
                context.Turer.Add(tur25);
                context.Turer.Add(tur26);
                context.Turer.Add(tur27);
                context.Turer.Add(tur28);
                context.Turer.Add(tur29);
                context.Turer.Add(tur30);
                context.Turer.Add(tur31);
                context.Turer.Add(tur32);
                context.Turer.Add(tur33);
                context.Turer.Add(tur34);
                context.Turer.Add(tur35);
                context.Turer.Add(tur36);
                context.Turer.Add(tur37);
                context.Turer.Add(tur38);
                context.Turer.Add(tur39);
                context.Turer.Add(tur40);
                context.Turer.Add(tur41);
                context.Turer.Add(tur42);
                context.Turer.Add(tur43);
                context.Turer.Add(tur44);
                context.Turer.Add(tur45);
                context.Turer.Add(tur46);
                context.Turer.Add(tur47);
                context.Turer.Add(tur48);
                context.Turer.Add(tur49);
                context.Turer.Add(tur50);
                context.Turer.Add(tur51);
                context.Turer.Add(tur52);
                context.Turer.Add(tur53);
                context.Turer.Add(tur54);
                context.Turer.Add(tur55);
                context.Turer.Add(tur56);
                context.Turer.Add(tur57);
                context.Turer.Add(tur58);
                context.Turer.Add(tur59);
                context.Turer.Add(tur60);
                context.Turer.Add(tur61);
                context.Turer.Add(tur62);
                context.Turer.Add(tur63);
                context.Turer.Add(tur64);
                context.Turer.Add(tur65);
                context.Turer.Add(tur66);
                context.Turer.Add(tur67);
                context.Turer.Add(tur68);
                context.Turer.Add(tur69);
                context.Turer.Add(tur70);
                context.Turer.Add(tur71);
                context.Turer.Add(tur72);
                context.Turer.Add(tur73);
                context.Turer.Add(tur74);
                context.Turer.Add(tur75);
                context.Turer.Add(tur76);
                context.Turer.Add(tur77);
                context.Turer.Add(tur78);
                context.Turer.Add(tur79);
                context.Turer.Add(tur80);
                context.Turer.Add(tur81);
                context.Turer.Add(tur82);
                context.Turer.Add(tur83);
                context.Turer.Add(tur84);
                context.Turer.Add(tur85);
                context.Turer.Add(tur86);
                context.Turer.Add(tur87);
                context.Turer.Add(tur88);
                context.Turer.Add(tur89);
                context.Turer.Add(tur90);
                context.Turer.Add(tur91);
                context.Turer.Add(tur92);
                context.Turer.Add(tur93);
                context.Turer.Add(tur94);
                context.Turer.Add(tur95);
                context.Turer.Add(tur96);
                context.Turer.Add(tur97);
                context.Turer.Add(tur98);
                context.Turer.Add(tur99);
                context.Turer.Add(tur100);
                context.Turer.Add(tur101);
                context.Turer.Add(tur102);
                context.Turer.Add(tur103);
                context.Turer.Add(tur104);
                context.Turer.Add(tur105);
                context.Turer.Add(tur106);
                context.Turer.Add(tur107);
                context.Turer.Add(tur108);

                /*---------OPPRETTER ADMIN-BRUKER--------*/

                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Admin1";
                byte[] salt = BussBestillingRepository.LagSalt();
                byte[] hash = BussBestillingRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Brukere.Add(bruker);

                context.SaveChanges();

            }
        }
    }
}

