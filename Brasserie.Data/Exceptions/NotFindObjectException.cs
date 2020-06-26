using System;
using System.Collections.Generic;
using System.Text;

namespace Brasserie.Data.Exceptions
{
    [Serializable]
    public class NotFindObjectException : Exception
    {
        public NotFindObjectException(string message) : base(message)
        {
        }
    }
}
