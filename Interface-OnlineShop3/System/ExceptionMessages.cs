using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.System
{
    public static class ExceptionMessages
    {
        //Order
        public static readonly string OrderDetailsNotFoundException = "Detaliile obiectului nu au fost gasite";
        public static readonly string NullOrderDetailException = "Detaiile obiectuluinu nu poate fi null";

        //OrderDetail
        public static readonly string OrderNotFoundException = "Order nu a fost gasit!";
        public static readonly string NullOrderException = "Order nu poate fi null";

        //Customer
        public static readonly string CustomerNotFoundException = "Customer nu a fost gasit";
    }
}
