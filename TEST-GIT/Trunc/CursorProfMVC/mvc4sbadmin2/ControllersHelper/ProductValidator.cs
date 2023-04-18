using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;

namespace nclprospekt.ControllersHelper
{
    public static class ProductValidator
    {
        internal static void validateProduktDetailsWO(ProduktDetailsWO productDetailsWO)
        {
            try
            {
                if (productDetailsWO == null) throw new Exception("Nie przesłano danych produktu do zapisania");
                if (productDetailsWO.produkt == null) throw new Exception("Nie przesłano danych produktu do zapisania");
                if (productDetailsWO.produkt.GRUPA_ID < 1) throw new Exception("Przesłane pozycje zawierają błędny identyfikator grupy.");
                if (productDetailsWO.produkt.ILOSC < 0) throw new Exception("Przesłane ilości nie powinny być ujemne!");
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}