using System;
using System.Collections.Generic;
using System.Text;

namespace Brasserie.Data.Exceptions
{
    [Serializable]
    public class NotEnoughQuantityException : Exception
    {
        public NotEnoughQuantityException(string message) : base(message) 
        {
        }
    }
}
