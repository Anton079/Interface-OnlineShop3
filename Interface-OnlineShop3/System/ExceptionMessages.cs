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
        public static readonly string NullCustomerException = "Cutomser nu poate fi null";

        //Products
        public static readonly string ProductWithoutStockException = "Cantitatea dorita este mai mare decat stockul!";
        public static readonly string ProductNotFoundException = "Produsul nu a fost gasit";
        public static readonly string NullProductException = "Product nu poate fi null";
        public static readonly string QuantityProductNotFoundException = "Quantity nu este suficient!";

        //Cos
        public static readonly string RemoveFromCosException = "Nu s-a putut da remove la cos!";
        public static readonly string OrderDetatilsDtoNotFound = "OrderDeatilsDto nu a fost gasit!";

        //admin
        public static readonly string AdminNotFoundException = "Admin nu a fost gasit";
        public static readonly string NullAdminException = "Admin nu poate fi null";

        //User
        public static readonly string NullUserException = "Userul nu poate fi null";
        public static readonly string UserNotFoundException = "Userul nu a fost gasit";
    }
}
