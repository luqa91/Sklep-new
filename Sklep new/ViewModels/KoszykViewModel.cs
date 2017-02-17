using Sklep_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_new.ViewModels
{
    public class KoszykViewModel
    {
        public List<PozycjaKoszyka> PozycjeKoszyka { get; set; }
        public decimal CenaCalkowita { get; set; }

    }
}