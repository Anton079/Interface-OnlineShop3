using Interface_OnlineShop3.OrderDetails.Exceptions;
using Interface_OnlineShop3.OrderDetails.Models;
using Interface_OnlineShop3.OrderDetails.Repository;
using Interface_OnlineShop3.Orders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.OrderDetails.Service
{
    public class OrderDetailsQueryService : IOrderDetailsQueryService
    {
        private IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsQueryService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _orderDetailsRepository.GetAll();
        }

        public OrderDetail FindOrderDetailsById(int id)
        {
            OrderDetail orderDetails = _orderDetailsRepository.FindById(id);

            if (orderDetails != null)
            {
                return orderDetails; 
            }
            else
            {
                throw new OrderDetailsNotFoundException();
            }
        }


        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(1, 10000000);

            while (FindOrderDetailsById(id) != null)
            {
                id = rand.Next(1, 10000000);
            }

            return id;
        }

        

    }
}
