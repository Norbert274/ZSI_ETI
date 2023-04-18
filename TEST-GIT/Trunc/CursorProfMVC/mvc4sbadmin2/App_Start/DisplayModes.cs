using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.Security;
using nclprospekt.Models;
using System.Web.WebPages;
using System.Collections.Specialized;

namespace nclprospekt
{
    public class DisplayModes
    {

        public static void RegisterDisplayModes(DisplayModeProvider displayModeProvider)
        {
            displayModeProvider.Modes.Insert(0, new DefaultDisplayMode("Mobile")
            {
               ContextCondition = (Context => IsMobile(Context.GetOverriddenUserAgent()))
            });
        }

        private static bool IsMobile(string useragentString)
        {
            return _useragenStringPartialIdentifiers.Cast<string>()
                        .Any(val => useragentString.IndexOf(val, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        private static readonly StringCollection _useragenStringPartialIdentifiers = new StringCollection
    {
        "Android",
        "Mobile",
        "Opera Mobi",
        "Samsung",
        "HTC",
        "Nokia",
        "Ericsson",
        "SonyEricsson",
        "iPhone"
    };


    }
}