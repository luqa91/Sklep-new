using System;
using System.Collections.Generic;
using System.Data.Entity;
using Sklep_new.Models;
using System.Data.Entity.Migrations;
using Sklep_new.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sklep_new.DAL
{
    public class KursyInitializer : MigrateDatabaseToLatestVersion<KursyContext, Configuration>
    {

        public static void SeedKursyData(KursyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId=1, NazwaKategorii="ASP", NazwaPlikuIkony="asp.net.png", OpisKategorii="opis asp net mvc"},
                new Kategoria() { KategoriaId=2, NazwaKategorii="java", NazwaPlikuIkony="java.png", OpisKategorii="opis java"},
                new Kategoria() { KategoriaId=3, NazwaKategorii="php", NazwaPlikuIkony="php.png", OpisKategorii="opis php"},
                new Kategoria() { KategoriaId=4, NazwaKategorii="html", NazwaPlikuIkony="html.png", OpisKategorii="opis html"},
                new Kategoria() { KategoriaId=5, NazwaKategorii="css", NazwaPlikuIkony="css.png", OpisKategorii="opis css"},
                new Kategoria() { KategoriaId=6, NazwaKategorii="xml", NazwaPlikuIkony="xml.png", OpisKategorii="opis xml"},
                new Kategoria() { KategoriaId=7, NazwaKategorii="c#", NazwaPlikuIkony="c.png", OpisKategorii="opis c#"},
            };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            var kursy = new List<Kurs>
            {
                new Kurs(){ AutorKursu="Tomek", TytulKursu="Asp.net mvc", KategoriaId=1, CenaKursu=99, Bestseller=true, NazwaPlikuObrazka="asp.net.png", DataDodania = DateTime.Now, Ukryty=false, OpisKursu="opis kursu", KursId=1 }
            };
            kursy.ForEach(k => context.Kursy.AddOrUpdate(k));
            context.SaveChanges();
        }

        public static void SeedUzytkownicy(KursyContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "wp@wp.pl";
            const string password = "Luki91!";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var result = userManager.Create(user, password);
            }

            // utworzenie roli Admin jeśli nie istnieje 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // dodanie uzytkownika do roli Admin jesli juz nie jest w roli
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }





    }
}