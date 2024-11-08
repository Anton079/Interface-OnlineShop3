using Interface_OnlineShop.Orders.Repository;
using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Orders.Service
{
    public class OrdersQueryService : IOrdersQueryService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersQueryService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public List<Order> GetAllOrders()
        {
            if (_ordersRepository.GetAll() != null)
            {
                return _ordersRepository.GetAll();
            }

            return null ;
        }

        public Order FindOrdersById(int id)
        {
            if (id != 0)
            {
                Order order = _ordersRepository.FindById(id);
                return order;
            }

            return null;
        }

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(1, 10000000);

            while (FindOrdersById(id) != null)
            {
                id = rand.Next(1, 10000000);
            }

            return id;
        }
    }
}
