using Sklep_new.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep_new.Controllers
{
    public class KursyController : Controller
    {
        private KursyContext db = new KursyContext();
        // GET: Kursy
        public ActionResult Index(string nazwaKategorii)
        {
            var kategoria = db.Kategorie.Include("Kursy").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategorii.ToUpper()).Single();
            var kursy = kategoria.Kursy.ToList();

            return View(kursy);
        }

        public ActionResult Lista(string nazwa)
        {
            return View();
        }

        public ActionResult MoreInfo(int id)
        {
            var kursy = db.Kursy.Find(id);

            return View(kursy);
        }

    }
}