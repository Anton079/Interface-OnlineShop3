using Interface_OnlineShop3.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Exceptions
{
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base(ExceptionMessages.ProductNotFoundException)
        {

        }
    }
}
