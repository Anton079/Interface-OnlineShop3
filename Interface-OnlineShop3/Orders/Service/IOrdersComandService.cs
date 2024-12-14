using Interface_OnlineShop3.Orders.Models;
using Interface_OnlineShop3.System;
using Interface_OnlineShop3.System.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Service
{
    public interface IOrdersCommandService
    {
        Order AddOrders(Order orders);

        int RemoveOrders(int id);

        Order UpdateOrders(int id, Order orders);

        public Order PlaceOrder(List<OrderDetailsDto> detailsDtos, int customerId, string customerAddress);

    }
}
