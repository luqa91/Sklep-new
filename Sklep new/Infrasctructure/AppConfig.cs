using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StronaSklep.Infrasctructure
{
    public class AppConfig
    {
        private static string _ikonyKategoriiFolderWzględny = ConfigurationManager.AppSettings["IkonyKategoriiFolder"];

        public static string IkonyKategoriiFolderWzględny
        {
            get
            {
                return _ikonyKategoriiFolderWzględny;
            }
        }

        private static string _obrazkiFolderWzględny = ConfigurationManager.AppSettings["ObrazkiFolder"];

        public static string ObrazkiFolderWzględny
        {
            get
            {
                return _obrazkiFolderWzględny;
            }
        }
    }
}