using Interface_OnlineShop3.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Customers.Exceptions
{
    internal class NullCustomerException : Exception
    {
        public NullCustomerException() : base(ExceptionMessages.NullCustomerException)
        {

        }
    }
}
