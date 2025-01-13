using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_OnlineShop3.System;

namespace Interface_OnlineShop3.Orders.Exceptions
{
    internal class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base(ExceptionMessages.OrderNotFoundException)
        {

        }

        public OrderNotFoundException(string message) : base(message)
        {

        }
    }
}
