﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class Bestilling
    {
        [Key]
        public int BId {get; set;}
        public int AntallBarn { get; set; }
        public int AntallVoksne { get; set; }
        public double TotalPris { get; set; }
        public virtual Turer Tur { get; set; }
        public virtual Kunde kunde { get; set; }
    }
}
