﻿using Sklep_new.DAL;
using Sklep_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_new.Infrasctructure
{
    public class KoszykManager
    {
        private KursyContext db;
        private ISessionManager session;
        public KoszykManager(ISessionManager session, KursyContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<PozycjaKoszyka> PobierzKoszyk()
        {

            List<PozycjaKoszyka> koszyk;
            if(session.Get<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz)==null)
            {
                koszyk = new List<PozycjaKoszyka>();
            }
            else
            {
                koszyk = session.Get<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz) as List<PozycjaKoszyka>;
            }


            return koszyk;
        }

        public void DodajdoKoszyka (int kursId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.Kurs.KursId == kursId);
            if (pozycjaKoszyka != null)
                pozycjaKoszyka.Ilosc++;
            else
            {
                var kursDoDodania = db.Kursy.Where(k => k.KursId == kursId).SingleOrDefault();
                if (kursDoDodania !=null)
                {
                    var nowaPozycjaKoszyka = new PozycjaKoszyka()
                    {
                        Kurs = kursDoDodania,
                        Ilosc = 1,
                        Wartosc = kursDoDodania.CenaKursu
                    };
                    koszyk.Add(nowaPozycjaKoszyka);
                }
            }

            session.Set(Const.KoszykSessionKlucz, koszyk);

        }

        public int UsunZKoszyka (int kursId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.Kurs.KursId == kursId);
            
            if(pozycjaKoszyka != null)
            {
                if(pozycjaKoszyka.Ilosc>1)
                {
                    pozycjaKoszyka.Ilosc--;
                    return pozycjaKoszyka.Ilosc;
                }
                else
                {
                    koszyk.Remove(pozycjaKoszyka);
                }
            }

            return 0;
        }

        public decimal PobierzWartoscKoszyka()
        {
            var koszyk = PobierzKoszyk();
            return koszyk.Sum(k => (k.Ilosc * k.Kurs.CenaKursu));
        }


        public int PobierzIloscPozycjiKoszyka()
        {
            var koszyk = PobierzKoszyk();
            int ilosc = koszyk.Sum(k => k.Ilosc);
            return ilosc;
        }

        public Zamowienie UtworzZamowienie(Zamowienie noweZamowienie, string userId)
        {
            var koszyk = PobierzKoszyk();
            noweZamowienie.DataDodania = DateTime.Now;
            //     noweZamowienie.userId = userId;

            db.Zamowienia.Add(noweZamowienie);

            if (noweZamowienie.PozycjeZamowienia == null)
                noweZamowienie.PozycjeZamowienia = new List<PozycjaZamowienia>();

            decimal koszykWartosc = 0;

            foreach(var koszykElement in koszyk)
            {
                var nowaPozycjaZamowienia = new PozycjaZamowienia()
                {
                    KursId = koszykElement.Kurs.KursId,
                    Ilosc = koszykElement.Ilosc,
                    CenaZakupu = koszykElement.Kurs.CenaKursu
                };
                koszykWartosc += (koszykElement.Ilosc * koszykElement.Kurs.CenaKursu);
                noweZamowienie.PozycjeZamowienia.Add(nowaPozycjaZamowienia);
            }

            noweZamowienie.WartoscZamowienia = koszykWartosc;
            db.SaveChanges();

            return noweZamowienie;

        }
        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz, null);
        }
    }
}