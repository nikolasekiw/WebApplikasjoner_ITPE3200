using System;
namespace KundeApp2.Model
{
    //Det er denne kunden her som vi overfører mellom klienten og tjeneren, den flate strukturen
     public class Kunde
     {
        //Denne kunde-klassen er modellen mellom klient og tjener og ikke definere databasen. 
        public int Id { get; set; }  // med Id som variabel blir dette en autoincrement i databasen (pr. default).
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }
       
     }
}
