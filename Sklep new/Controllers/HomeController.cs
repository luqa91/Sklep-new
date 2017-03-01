using Sklep_new.ViewModels;
using Sklep_new.DAL;
using System;
using System.Linq;
using System.Web.Mvc;
using Sklep_new.Infrasctructure;
using System.Collections.Generic;
using Sklep_new.Models;

namespace Sklep_new.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        public ActionResult Index()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            List<Kurs> nowosci;
            if (cache.IsSet(Const.NowosciCacheKey))
            {
                nowosci = cache.Get(Const.NowosciCacheKey) as List<Kurs>;
            }
            else
            {
            nowosci = db.Kursy.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
                cache.Set(Const.NowosciCacheKey, nowosci, 60);

            }

            List<Kurs> bestsellery;
            if (cache.IsSet(Const.BestselleryCacheKey))
            {
                bestsellery = cache.Get(Const.BestselleryCacheKey) as List<Kurs>;
            }
            else
            {
                bestsellery = db.Kursy.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Const.BestselleryCacheKey, bestsellery, 60);

            }


            List<Kategoria> kategorie;
            if (cache.IsSet(Const.KategorieCacheKey))
            {
                kategorie = cache.Get(Const.KategorieCacheKey) as List<Kategoria>;
            }
            else
            {
                kategorie = db.Kategorie.ToList();
                cache.Set(Const.KategorieCacheKey, kategorie, 60);
            }

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



        



    }
}