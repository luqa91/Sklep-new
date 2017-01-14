using Sklep_new.Models;
using System.Collections.Generic;

namespace Sklep_new.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable <Kategoria> Kategorie { get; set; }
        public IEnumerable<Kurs> Nowości { get; set; }
        public IEnumerable<Kurs> Bestsellery { get; set; }


    }
}