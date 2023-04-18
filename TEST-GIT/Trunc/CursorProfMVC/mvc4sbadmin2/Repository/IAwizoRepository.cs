using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
         public interface IAwizoRepository
        {
             RezultatObject AwizoDostawcaUsun(byte[] sesja, int dostawca_id);
             RezultatObject AwizoDostawcaZapisz(byte[] sesja, AwizoDostawca dostawca);
             AwizoWO AwizoPodgladWczytaj(byte[] sesja, int awizoID);
             AwizoWO AwizoDostawcyLista(byte[] sesja);

             AwizoWO AwizoSKULista(byte[] sesja);
                          
             AwizoWO AwizoEdycjaWczytaj(byte[] sesja, int awizoID);
             AwizoDostawca AwizoDostawcaSzczegoly(byte[] sesja, int dostawca_id);

             RezultatObject AwizoRealizuj(byte[] sesja, AwizoWO awizoWO);
             RezultatObject AwizoEdycjaZapisz(byte[] sesja, AwizoWO awizo);
             
             AwizoListaWO AwizaLista(byte[] sesja, DateTime data_utworzenia_od, DateTime data_utworzenia_do,
                                        DateTime data_planowana_dostawy_od, DateTime data_planowana_dostawy_do, string nr_awiza, string nr_po, string dostawca,
                                        string qguar_za, string qguar_dostawa, string sortowanie,
                                        int strona, int ilosc_na_stronie, bool rosnaco, string strXmlStatusy, int user_id, string sku);
        }

}
