using MvcSiteMapProvider;
using Sklep_new.DAL;
using Sklep_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_new.Infrasctructure
{
    public class KursySzczegolyDynamicNodeProvider: DynamicNodeProviderBase
    {
        private KursyContext db = new KursyContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue =new List<DynamicNode>();

            foreach (Kurs kurs in db.Kursy)
            {
                DynamicNode node = new DynamicNode();
                node.Title = kurs.TytulKursu;
                node.Key = "Kurs_" + kurs.KursId;
                node.ParentKey = "Kategoria_" + kurs.KategoriaId;
                node.RouteValues.Add("id", kurs.KursId);
                returnValue.Add(node);
            }

            return returnValue;


        }


    }
}