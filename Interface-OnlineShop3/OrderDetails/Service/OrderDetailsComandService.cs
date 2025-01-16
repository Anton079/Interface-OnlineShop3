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
    public class OrderDetailsCommandService : IOrderDetailsCommandService
    {
        private IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsCommandService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public OrderDetail AddOrderDetails(OrderDetail orderDetails)
        {
            if(_orderDetailsRepository.AddOrderDetail(orderDetails) != null)
            {
                return orderDetails;
            }
            else
            {
                throw new OrderDetailsNotFoundException();
            }
        }

        public int RemoveOrderDetails(int id)
        {
            if(_orderDetailsRepository.Remove(id) != null)
            {
                return id;
            }
            else
            {
                throw new OrderDetailsNotFoundException();
            }
        }

        public OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails)
        {
            if (id == -1)
            {
                throw new OrderNotFoundException();
            }

            _orderDetailsRepository.UpdateOrderDetails(id, orderDetails);
            return orderDetails;
        }
    }
}
