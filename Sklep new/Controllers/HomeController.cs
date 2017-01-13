using StronaSklep.DAL;
using StronaSklep.Models;
using StronaSklep.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep_new.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        public ActionResult Index()
        {

            var kategorie = db.Kategorie.ToList();
            var nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
            var bestsellery = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();

            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowości = nowosci,
                Bestsellery = bestsellery,

            };
            return View(vm);
        }


        public ActionResult Kategoria()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Cart()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult StronyStatyczne(string nazwa)
        {
            var kategorie = db.Kategorie.ToList();
            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
            };
            return View(nazwa, vm);
        }

        public ActionResult test()
        {
            {

                var kategorie = db.Kategorie.ToList();
                var nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
                var bestsellery = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();

                var m = new HomeViewModel()
                {
                    Kategorie = kategorie,
                    Nowości = nowosci,
                    Bestsellery = bestsellery,

                };
                return View("test", m);
            }
        }
    }
}