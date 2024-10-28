using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.OrderDetails.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private List<OrderDetail> orderDetailsList;

        public OrderDetailsRepository()
        {
            orderDetailsList = new List<OrderDetail>();
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
                        OrderDetail orderDetails = new OrderDetail(line);
                        this.orderDetailsList.Add(orderDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folder = Path.Combine(currentDirectory, "data");
            string file = Path.Combine(folder, "OrderDetail");
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
                Console.WriteLine(ex);
            }
        }

        public string ToSaveAll()
        {
            string save = "";

            for (int i = 0; i < orderDetailsList.Count; i++)
            {
                save += orderDetailsList[i].ToString();

                if (i < orderDetailsList.Count - 1)
                {
                    save += "\n";
                }
            }
            return save;
        }

        // CRUD 

        public List<OrderDetail> GetAll()
        {
            return orderDetailsList;
        }

        public OrderDetail Add(OrderDetail orderDetails)
        {
            if(orderDetails != null)
            {
                this.orderDetailsList.Add(orderDetails);
                this.SaveData();
                return orderDetails;
            }

            return null;
        }

        public OrderDetail Remove(int id)
        {
            OrderDetail od = FindById(id);

            if(od != null)
            {
                orderDetailsList.Remove(od);
                this.SaveData();
                return od;
            }
            return null;
        }

        public OrderDetail FindById(int id)
        {
            foreach (OrderDetail orderDetails in orderDetailsList)
            {
                if (orderDetails.Id == id)
                {
                    return orderDetails;
                }
            }
            return null;
        }

        public OrderDetail UpdateOrderDetails(int id,OrderDetail orderDetails)
        {
            OrderDetail orderDetailsUpdate = FindById(id);

            if (orderDetailsUpdate != null)
            {
                orderDetailsUpdate.OrderId = orderDetails.OrderId;
                orderDetailsUpdate.ProductId = orderDetails.ProductId;
                orderDetailsUpdate.Price = orderDetails.Price;
                orderDetailsUpdate.Quantity = orderDetails.Quantity;

                this.SaveData();
            }
            return orderDetailsUpdate;
        }
    }
}
