using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.OrderDetails.Repository
{
    public interface IOrderDetailsRepository
    {
        List<OrderDetail> GetAll();

        OrderDetail AddOrderDetail(OrderDetail orderDetails);

        OrderDetail Remove(int id);

        OrderDetail FindById(int id);

        OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails);

    }
}
