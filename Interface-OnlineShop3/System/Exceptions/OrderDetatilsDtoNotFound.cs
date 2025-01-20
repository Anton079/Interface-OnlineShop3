using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.System.Exceptions
{
    internal class OrderDetatilsDtoNotFound : Exception
    {
        public OrderDetatilsDtoNotFound() : base(ExceptionMessages.OrderDetatilsDtoNotFound)
        {

        }
    }
}
