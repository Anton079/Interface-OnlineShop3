﻿using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Products.Service
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

            if(products != null)
            {
                return _prodRepository.getAll();
            }
            return null;
        }

        public Product FindProductById(int id)
        {
            Product product = _prodRepository.FindById(id);

            return product;
        }
    }
}
