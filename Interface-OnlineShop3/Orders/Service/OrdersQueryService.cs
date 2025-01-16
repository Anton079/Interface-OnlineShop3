using Interface_OnlineShop3.Orders.Exceptions;
using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.Orders.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Service
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

            return null;
        }

        public Order FindOrdersById(int id)
        {
            Order order = _ordersRepository.FindById(id);
            if (order != null)
            {
                return order;
            }
            else
            {
                throw new OrderNotFoundException();
            }
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
