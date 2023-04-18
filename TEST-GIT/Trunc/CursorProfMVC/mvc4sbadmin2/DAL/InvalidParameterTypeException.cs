using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.DAL
{
    [Serializable]
    public class InvalidParameterTypeException : Exception
    {
        public InvalidParameterTypeException()
            : base()
        {

        }

        public InvalidParameterTypeException(string message)
            : base(message)
        {

        }

        public InvalidParameterTypeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected InvalidParameterTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }

    }
}