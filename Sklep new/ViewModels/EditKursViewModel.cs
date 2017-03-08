using Sklep_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_new.ViewModels
{
    public class EditKursViewModel
    {
        public Kurs Kurs { get; set; }
        public IEnumerable<Kategoria> Kategoria { get; set; }

        public bool? Potwierdzenie { get; set; }

    }
}