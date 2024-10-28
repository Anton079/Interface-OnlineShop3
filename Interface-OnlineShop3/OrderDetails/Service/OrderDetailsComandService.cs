using Interface_OnlineShop.OrderDetails.Repository;
using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.OrderDetails.Service
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
            if (orderDetails != null && _orderDetailsRepository.FindById(orderDetails.Id) == null)
            {
                _orderDetailsRepository.AddOrderDetail(orderDetails);
                return orderDetails;
            }
            return null;
        }

        public int RemoveOrderDetails(int id)
        {

            if (id != -1)
            {
                _orderDetailsRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails)
        {
            if (orderDetails != null && _orderDetailsRepository.FindById(id) != null)
            {
                _orderDetailsRepository.UpdateOrderDetails(id ,orderDetails);
                return orderDetails;
            }
            return null;
        }
    }
}
