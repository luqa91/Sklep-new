using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_new.Models
{
    public class PozycjaKoszyka
    {
        public Kurs Kurs { get; set; }
        public int Ilosc { get; set; }
        public decimal Wartosc { get; set; }
    }
}