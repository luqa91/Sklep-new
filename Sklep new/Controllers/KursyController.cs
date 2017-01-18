using Sklep_new.DAL;
using System.Linq;
using System.Web.Mvc;

namespace Sklep_new.Controllers
{
    public class KursyController : Controller
    {
        private KursyContext db = new KursyContext();
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista(string nazwaKategorii)
        {
            var kategoria = db.Kategorie.Include("Kursy").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategorii.ToUpper()).Single();
            var kursy = kategoria.Kursy.ToList();
            return View(kursy);
        }

        public ActionResult MoreInfo(int id)
        {
            var kursy = db.Kursy.Find(id);

            return View(kursy);
        }

        [ChildActionOnly]
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();

            return PartialView("_KategorieMenu", kategorie);
        }

    }
}