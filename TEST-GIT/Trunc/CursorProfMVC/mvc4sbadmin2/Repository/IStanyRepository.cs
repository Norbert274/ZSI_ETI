using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
    public interface IStanyRepository
    {

        StanyWO LoadStanyWO(byte[] sesja, int magazyn_id, string grupy, string marki, string branze, string kategorie, string sku, string nazwa, int strona, int ilosc_na_stronie, string sortowanie, bool rosnaco, bool czy_niezerowe, bool onlynew);
        ProduktZdjeciaWO ShowGallery(byte[] sesja, int sku_id);
       ProduktDetailsWO ProductDetails(byte[] sesja, int magazyn_id, int sku_id, int grupa_id);
        StanyWO LoadDictionaries(byte[] sesja);
        int PobierzDomyslnyMagazynID(byte[] sesja);
    }
}