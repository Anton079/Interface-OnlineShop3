﻿using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> productsList;

        public ProductRepository()
        {
            productsList = new List<Product>();
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
                        Product products = new Product(line);
                        this.productsList.Add(products);
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

            string file = Path.Combine(folder, "Products");

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

            for (int i = 0; i < productsList.Count; i++)
            {
                save += productsList[i].ToString();

                if (i < productsList.Count - 1)
                {
                    save += "\n";
                }
            }

            return save;
        }

        //crud

        public List<Product> getAll()
        {
            return productsList;
        }

        public Product Add(Product products)
        {
            Product prod = FindById(products.Id);

            if(prod == null)
            {
                this.productsList.Add(products);
                this.SaveData();
            }

            return products;
        }

        public Product Remove(int id)
        {
            Product pro = FindById(id);

            if(pro != null)
            {
                this.productsList.Remove(pro);
                this.SaveData();
                return pro;
            }

            return null;
        }

        public Product FindById(int id)
        {
            foreach (Product product in productsList)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        public Product UpdateProducts(int id, Product updatedProduct)
        {
            Product existingProduct = FindById(id);

            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Descriptions = updatedProduct.Descriptions;
                existingProduct.CreateDate = updatedProduct.CreateDate;
                existingProduct.Stock = updatedProduct.Stock;

                this.SaveData();
            }
            return existingProduct;
        }
    }
}