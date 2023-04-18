using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace nclprospekt.Exceptions
{
    public static class SesjaExceptionCheck
    {
        const string CONST_SESJA_EXCEPTION = "identyfikator sesji";
        const string CONST_SESJA_EXCEPTION_SP = "Błąd sesji";

        public static int SesjaCheck(int status, string message)
        {
            if (status == -1)
            {
                if (message.IndexOf(CONST_SESJA_EXCEPTION) > -1)
                {
                    status = -2;
                }
                else if (message.IndexOf(CONST_SESJA_EXCEPTION_SP) > -1)
                {
                    status = -2;
                }
            };

            return status;
        }

        public static void SesjaCheckThrowIfError(int status, string message)
        {
            if (status == 0) return;

            if (message.ToLower().IndexOf(CONST_SESJA_EXCEPTION) > -1
                || message.ToLower().IndexOf(CONST_SESJA_EXCEPTION_SP) > -1)
            {
                throw new SesjaException(message);
            }

            if (status == -1)
            {
                throw new Exception(message);
            }
        }


    }

    [Serializable]
    public class SesjaException : Exception
    {
        public SesjaException()
            : base() { }

        public SesjaException(string message)
            : base(message) { }

        public SesjaException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public SesjaException(string message, Exception innerException)
            : base(message, innerException) { }

        public SesjaException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected SesjaException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}