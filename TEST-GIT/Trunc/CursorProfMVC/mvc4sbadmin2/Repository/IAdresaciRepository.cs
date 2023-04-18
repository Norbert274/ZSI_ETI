using nclprospekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nclprospekt.Repository
{
    public interface IAdresaciRepository
    {
        Adresaci pobierzAdresatow(byte[] sesja);
    }
}
