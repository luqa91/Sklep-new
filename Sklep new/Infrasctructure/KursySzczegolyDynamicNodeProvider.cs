﻿using MvcSiteMapProvider;
using Sklep_new.DAL;
using Sklep_new.Models;
using System.Collections.Generic;

namespace Sklep_new.Infrasctructure
{
    public class KursySzczegolyDynamicNodeProvider: DynamicNodeProviderBase
    {
        private KursyContext db = new KursyContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue =new List<DynamicNode>();

            foreach (Kurs kurs in db.Kursy)
            {
                DynamicNode nodee = new DynamicNode();
                nodee.Title = kurs.TytulKursu;
                nodee.Key = "Kurs_" + kurs.KursId;
                nodee.ParentKey = "Kategoria_" + kurs.KategoriaId;
                nodee.RouteValues.Add("id", kurs.KursId);
                returnValue.Add(nodee);
            }

            return returnValue;


        }


    }
}