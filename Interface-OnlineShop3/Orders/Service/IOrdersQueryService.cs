using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Orders.Service
{
    public interface IOrdersQueryService
    {
        List<Order> GetAllOrders();

        Order FindOrdersById(int id);

        int GenerateId();
    }
}
