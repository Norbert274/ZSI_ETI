using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt
{
    public static class NclHelper
    {
        public static T CastTo<T>(object value)
        {
            return value != DBNull.Value ? (T)value : default(T);
        }
    }
}