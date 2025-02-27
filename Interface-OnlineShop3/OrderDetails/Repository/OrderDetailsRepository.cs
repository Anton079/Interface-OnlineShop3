﻿using Interface_OnlineShop3.OrderDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.OrderDetails.Repository
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
                        orderDetailsList.Add(orderDetails);
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

        public OrderDetail AddOrderDetail(OrderDetail orderDetails)
        {
            orderDetailsList.Add(orderDetails);
            SaveData();
            return orderDetails;
        }

        public OrderDetail Remove(int id)
        {
            OrderDetail od = FindById(id);

            orderDetailsList.Remove(od);
            SaveData();
            return od;
        }

        public OrderDetail FindById(int id)
        {
            if(id != -1)
            {
                foreach (OrderDetail orderDetails in orderDetailsList)
                {
                    if (orderDetails.Id == id)
                    {
                        return orderDetails;
                    }
                }
            }
            return null;
        }

        public OrderDetail UpdateOrderDetails(int id, OrderDetail orderDetails)
        {
            OrderDetail orderDetailsUpdate = FindById(id);

            orderDetailsUpdate.OrderId = orderDetails.OrderId;
            orderDetailsUpdate.ProductId = orderDetails.ProductId;
            orderDetailsUpdate.Price = orderDetails.Price;
            orderDetailsUpdate.Quantity = orderDetails.Quantity;

            SaveData();
            return orderDetailsUpdate;
        }

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(0, 1000000);

            while(FindById(id) != null)
            {
                id = rand.Next(0, 100000);
            }
            return id;
        }
    }
}
