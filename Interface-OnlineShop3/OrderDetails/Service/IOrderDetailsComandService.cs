using Interface_OnlineShop.OrderDetails.Repository;
using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.OrderDetails.Service
{
    public interface IOrderDetailsCommandService
    {
        OrderDetail AddOrderDetails(OrderDetail orderDetails);

        int RemoveOrderDetails(int id);

        OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails);

    }
}
