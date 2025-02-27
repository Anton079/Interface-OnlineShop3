﻿using Interface_OnlineShop3.Orders.Exceptions;
using Interface_OnlineShop3.Products.Exceptions;
using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Service
{
    public class ProductQueryService : IProductQueryService
    {
        private IProductRepository _prodRepository;

        public ProductQueryService(IProductRepository proQRepository)
        {
            _prodRepository = proQRepository;
        }

        //crud

        public List<Product> getAll()
        {
            List<Product> products = new List<Product>();

            if (products != null)
            {
                return _prodRepository.getAll();
            }
            return null;
        }

        public Product FindProductById(int id)
        {
            Product product = _prodRepository.FindById(id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public int FindProductIdByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);
            if (product != null)
            {
                return product.Id;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public int FindProductStockByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);
            if (product != null)
            {
                return product.Stock;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public int FindProductPriceByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);
            if (product != null)
            {
                return product.Price;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public string FindProductNameById(int idProduct)
        {
            Product product = _prodRepository.FindById(idProduct);

            if (product != null)
            {
                return product.Name;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public List<Product> FindProductsByName(string searchTerm)
        {
            List<Product> products = getAll();
            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (product.Name.ToLower().Contains(searchTerm) || product.Descriptions.ToLower().Contains(searchTerm))
                {
                    filteredProducts.Add(product);
                }
            }

            if (filteredProducts.Count == 0)
            {
                throw new QuantityProductNotFoundException();
            }

            return filteredProducts;

        }

        public List<Product> FindMinAndMaxProductsPrice(int minPrice, int maxPrice)
        {
            List<Product> products = getAll();
            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (product.Price >= minPrice && product.Price <= maxPrice)
                {
                    filteredProducts.Add(product);
                }
            }

            if (filteredProducts.Count == 0)
            {
                throw new QuantityProductNotFoundException();
            }

            return filteredProducts;
        }

    }
}
