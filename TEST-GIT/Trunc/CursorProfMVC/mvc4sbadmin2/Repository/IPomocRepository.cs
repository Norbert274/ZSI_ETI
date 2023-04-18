using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using System.Data;

namespace nclprospekt.Repository
{
    public interface IPomocRepository
    {
        RezultatObject SendMail(byte[] sesja, string tytul, string tresc, string nazwaPliku, int zrodloMaila);
        RezultatObject SaveAttachment(byte[] sesja, string nazwaPliku, byte[] plik);
        KontaktDaneDodatkowe PobierzParametrPoNazwie(byte[] sesja, string nazwaParametru);
        PomocPobierzPlik PobierzPlik(byte[] sesja, string nazwaPliku);
        PomocPlikiListaWO PobierzListePlikow(byte[] sesja);
    }
}