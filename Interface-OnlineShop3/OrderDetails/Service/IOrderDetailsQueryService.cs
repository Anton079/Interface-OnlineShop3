using Interface_OnlineShop.OrderDetails.Repository;
using Interface_OnlineShop.OrderDetails.Service;
using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.OrderDetails.Service
{
    public interface IOrderDetailsQueryService
    {
        List<OrderDetail> GetAllOrderDetails();

        OrderDetail FindOrderDetailsById(int id);

    }
}
