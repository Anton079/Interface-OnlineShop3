using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_OnlineShop3.System;

namespace Interface_OnlineShop3.Products.Exceptions
{
    internal class QuantityProductNotFoundException : Exception
    {
        public QuantityProductNotFoundException() : base(ExceptionMessages.QuantityProductNotFoundException)
        {

        }
    }
}
