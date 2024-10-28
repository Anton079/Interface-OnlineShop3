using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Orders.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private List<Order> ordersList;

        public OrdersRepository()
        {
            ordersList = new List<Order>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GetFilePath()))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order orders = new Order(line);
                        this.ordersList.Add(orders);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folder = Path.Combine(currentDirectory, "data");
            string file = Path.Combine(folder, "Order");
            return file;
        }

        private void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetFilePath()))
                {
                    sw.WriteLine(ToSaveAll());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string ToSaveAll()
        {
            string save = "";
            for (int i = 0; i < ordersList.Count; i++)
            {
                save += ordersList[i].ToString();

                if (i < ordersList.Count - 1)
                {
                    save += "\n";
                }
            }
            return save;
        }

        // CRUD 

        public List<Order> GetAll()
        {
            return ordersList;
        }

        public Order AddOrder(Order orders)
        {
            if(orders != null)
            {
                this.ordersList.Add(orders);
                this.SaveData();
                return orders;
            }

            return null;
        }

        public Order Remove(int id)
        {
            foreach(Order x in ordersList)
            {
                Order order = FindById(id);

                if (x.Id == order.Id)
                {
                    ordersList.Remove(order);
                    this.SaveData();
                    return order;
                }
            }
            return null;
        }

        public Order FindById(int id)
        {
            foreach (Order orders in ordersList)
            {
                if (orders.Id == id)
                {
                    return orders;
                }
            }
            return null;
        }

        public Order UpdateOrders(int id, Order orders)
        {
            Order ordersUpdate = FindById(id);

            if (ordersUpdate != null)
            {
                ordersUpdate.CustomerId = orders.CustomerId;
                ordersUpdate.Amount = orders.Amount;
                ordersUpdate.ShippingAddress = orders.ShippingAddress;

                this.SaveData();
                return ordersUpdate;
            }
            return null;
        }
    }
}
