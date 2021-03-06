﻿using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sklep_new.DAL;
using Sklep_new.Infrasctructure;
using Sklep_new.Models;
using Sklep_new.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sklep_new.Controllers
{
    public class CartController : Controller
    {
        private KoszykManager koszykManager;

        private ISessionManager SessionManager { get; set; }
        private KursyContext db;

        public CartController()
        {
            db = new KursyContext();
            SessionManager = new SessionManager();
            koszykManager = new KoszykManager(SessionManager, db);
        }



        // GET: Cart
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykManager.PobierzKoszyk();
            var cenaCalkowita = koszykManager.PobierzWartoscKoszyka();
            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita = cenaCalkowita
            };

            return View(koszykVM);
        }

        public ActionResult BuyNow(int id)
        {
            koszykManager.DodajdoKoszyka(id);

            return RedirectToAction("Index");
        }

        public int PobierzIloscElementowKoszyka()
        {
            return koszykManager.PobierzIloscPozycjiKoszyka();
        }



        public ActionResult UsunZKoszyka(int kursId2)
        {


            int iloscPozycji = koszykManager.UsunZKoszyka(kursId2);
            int iloscPozycjiKoszyka = koszykManager.PobierzIloscPozycjiKoszyka();
            decimal wartoscKOszyka = koszykManager.PobierzWartoscKoszyka();

            var wynik = new KoszykUsuwanieViewModel
            {
                IdPozycjiUsuwanej = kursId2,
                IloscPozycjiUsuwanej = iloscPozycji,
                KoszykCenaCalkowita = wartoscKOszyka,
                KoszykIloscPozycji = iloscPozycjiKoszyka,

            };
            return Json(wynik);
        }

        public async Task <ActionResult> Zaplac()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var zamowienie = new Zamowienie
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy = user.DaneUzytkownika.KodPocztowy,
                    Email = user.DaneUzytkownika.Email,
                    Telefon = user.DaneUzytkownika.Telefon,
                };
                return View(zamowienie);
            }
            else
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("Zaplac", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult>Zaplac(Zamowienie zamowienieSzczegoly)
        {
            if (ModelState.IsValid)
            {
                //pobieramy id uzytkownika aktualnie zalogowanego
                var userId = User.Identity.GetUserId();

                //utworzenie obiektu zamowienia na poidstawie tego co mamy w koszyku
                var newOrder = koszykManager.UtworzZamowienie(zamowienieSzczegoly, userId);

                //szczegóły użytkownika - aktualizacja danych
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);

                //opróżnimy nasz koszyk zakupów
                koszykManager.PustyKoszyk();





                string url = Url.Action("PotwierdzenieZamowieniaEmail", "Cart", new { zamowienieid = newOrder.ZamowienieID, nazwisko = newOrder.Nazwisko }, Request.Url.Scheme);
                BackgroundJob.Enqueue(() => Call(url));







                return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
                return View(zamowienieSzczegoly);

        }

        public static void Call (string url)
        {
            var req = HttpWebRequest.Create(url);
            req.GetResponseAsync();
        }

        public ActionResult PotwierdzenieZamowieniaEmail (int zamowienieid, string nazwisko)
        {

            var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").
                Include("PozycjeZamowienia.Kurs").SingleOrDefault
                (o => o.ZamowienieID == zamowienieid && o.Nazwisko == nazwisko);

            if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var email = new PotwierdzenieZamowieniaEmail();
            email.To = zamowienie.Email;
            email.From = "Kaczmareek.Lukasz@gmail.com";
            email.Wartosc = zamowienie.WartoscZamowienia;
            email.NumerZamowienia = zamowienie.ZamowienieID;
            email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;

            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        public ActionResult PotwierdzenieZamowienia()
        {
            var name = User.Identity.Name;
           // Logger.Info("Strona koszyk | Potwierdzenie | " + name);
            return View();
        }


        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }


        }


    }
}
