using System;
using System.Collections.Generic;
using System.Text;

namespace Brasserie.Data.Exceptions
{
    [Serializable]
    public class HttpBodyException : Exception
    {
        public HttpBodyException(string message) : base(message)
        {
        }
    }
}
