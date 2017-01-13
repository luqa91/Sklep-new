﻿using StronaSklep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StronaSklep.DAL
{
    public class KursyContext : DbContext
    {
        public KursyContext() : base("KursyContext")
        {

        }

        static KursyContext()
        {
            Database.SetInitializer <KursyContext>( new KursyInitializer());

        }


        public DbSet<Kurs> Kursy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            //using System.data.entity.model configuration.conventions;
            //wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            //zamiast Kategorie zostałyby stworzone tabele o nazwie Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
            base.OnModelCreating(modelBuilder);

        }
    }
}