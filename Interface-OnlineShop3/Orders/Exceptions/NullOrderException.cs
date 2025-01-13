using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_OnlineShop3.System;

namespace Interface_OnlineShop3.Orders.Exceptions
{
    internal class NullOrderException : Exception
    {
        public NullOrderException() : base(ExceptionMessages.NullOrderException)
        {

        }

        public NullOrderException(string message) : base(message)
        {

        }
    }
}
