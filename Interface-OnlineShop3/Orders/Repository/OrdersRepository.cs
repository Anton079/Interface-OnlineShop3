using Interface_OnlineShop3.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Repository
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
                        ordersList.Add(orders);
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

        public void SaveData()
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
            ordersList.Add(orders);
            SaveData();
            return orders;
        }

        public Order Remove(int id)
        {
            Order order = FindById(id);

            ordersList.Remove(order);
            SaveData();
            return order;
        }

        public Order FindById(int id)
        {
            if(id != -1)
            {
                foreach (Order orders in ordersList)
                {
                    if (orders.Id == id)
                    {
                        return orders;
                    }
                }
            }
            return null;
        }

        public Order UpdateOrders(int id, Order orders)
        {
            Order ordersUpdate = FindById(id);

            ordersUpdate.CustomerId = orders.CustomerId;
            ordersUpdate.Amount = orders.Amount;
            ordersUpdate.ShippingAddress = orders.ShippingAddress;

            SaveData();
            return ordersUpdate;
        }

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(0, 1000000);

            while (FindById(id) != null)
            {
                id = rand.Next(0, 100000);
            }
            return id;
        }

    }
}
