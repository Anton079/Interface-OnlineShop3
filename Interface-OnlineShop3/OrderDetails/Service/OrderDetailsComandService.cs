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
    public class OrderDetailsCommandService : IOrderDetailsCommandService
    {
        private IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsCommandService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public OrderDetail AddOrderDetails(OrderDetail orderDetails)
        {
            try
            {
                _orderDetailsRepository.AddOrderDetail(orderDetails);
                return orderDetails;
            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int RemoveOrderDetails(int id)
        {
            try
            {
                _orderDetailsRepository.Remove(id);
                return id;
            }catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails)
        {
            try
            {
                if(id  == -1)
                {
                    throw new OrderNotFoundException();
                }

                _orderDetailsRepository.UpdateOrderDetails(id, orderDetails);
                return orderDetails;
            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
