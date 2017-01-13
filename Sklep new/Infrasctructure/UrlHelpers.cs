﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StronaSklep.Infrasctructure
{
    public static class UrlHelpers
    {
        public static string IkonyKategoriiSciezka(this UrlHelper helper, string nazwaIkonyKategorii)
        {
            var IkonyKategoriiFolder = AppConfig.IkonyKategoriiFolderWzględny;
            var sciezka = Path.Combine(IkonyKategoriiFolder, nazwaIkonyKategorii);
            var sciezkaBezwzgledna = helper.Content(sciezka);

            return sciezkaBezwzgledna;
        }

        public static string ObrazkiSciezka(this UrlHelper helper, string nazwaObrazka)
        {
            var ObrazkiFolder = AppConfig.ObrazkiFolderWzględny;
            var sciezka = Path.Combine(ObrazkiFolder, nazwaObrazka);
            var sciezkaBezwzgledna = helper.Content(sciezka);

            return sciezkaBezwzgledna;
        }
    }
}