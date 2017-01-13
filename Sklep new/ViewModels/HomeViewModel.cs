using StronaSklep.Models;
using System.Collections.Generic;

namespace StronaSklep.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable <Kategoria> Kategorie { get; set; }
        public IEnumerable<Kurs> Nowości { get; set; }
        public IEnumerable<Kurs> Bestsellery { get; set; }


    }
}