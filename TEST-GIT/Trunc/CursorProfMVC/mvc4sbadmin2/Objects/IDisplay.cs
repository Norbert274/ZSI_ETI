using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Objects
{
    public interface IDisplay
    {
        void Write(string message);
        void SetColor(string color);
        void ResetColor();
    }
}