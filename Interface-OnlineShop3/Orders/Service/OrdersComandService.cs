using Interface_OnlineShop.Orders.Repository;
using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Orders.Service
{
    public class OrdersCommandService : IOrdersCommandService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersCommandService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Order AddOrders(Order orders)
        {
            if (orders != null)
            {
                _ordersRepository.AddOrder(orders);
                return orders;
            }
            return null;
        }

        public int RemoveOrders(int id)
        {
            if (_ordersRepository.FindById(id) != null)
            {
                _ordersRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public Order UpdateOrders(int id,Order orders)
        {
            if (orders != null)
            {
                _ordersRepository.UpdateOrders(id,orders);
                return orders;
            }
            return null;
        }
    }
}
