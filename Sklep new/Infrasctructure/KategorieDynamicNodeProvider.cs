using MvcSiteMapProvider;
using Sklep_new.DAL;
using Sklep_new.Models;
using System.Collections.Generic;

namespace Sklep_new.Infrasctructure
{
    public class KategorieDynamicNodeProvider: DynamicNodeProviderBase
    {
        private KursyContext db = new KursyContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Kategoria kategoria in db.Kategorie)
            {
                DynamicNode nodee = new DynamicNode();
                nodee.Title =kategoria.NazwaKategorii;
                nodee.Key = "Kategoria_" + kategoria.KategoriaId;
                nodee.RouteValues.Add("nazwaKategorii", kategoria.NazwaKategorii);
                returnValue.Add(nodee);
            }

            return returnValue;


        }


    }
}