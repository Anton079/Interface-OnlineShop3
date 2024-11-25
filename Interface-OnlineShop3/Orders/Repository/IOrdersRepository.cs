using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Repository
{
    public interface IOrdersRepository
    {
        List<Order> GetAll();

        Order AddOrder(Order orders);

        Order Remove(int id);

        Order FindById(int id);

        Order UpdateOrders(int id, Order orders);

    }
}
