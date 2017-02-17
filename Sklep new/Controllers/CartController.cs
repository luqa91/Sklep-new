using Sklep_new.DAL;
using Sklep_new.Infrasctructure;
using Sklep_new.ViewModels;
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

    }
}
