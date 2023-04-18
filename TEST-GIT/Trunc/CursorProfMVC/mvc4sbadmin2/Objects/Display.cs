using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Objects
{
    class ConsoleDisplay : IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void SetColor(string color)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        public void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}