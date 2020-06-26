using System;
using System.Collections.Generic;
using System.Text;

namespace Brasserie.Data.Exceptions
{
    [Serializable]
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message)
        {
        }
    }
}
