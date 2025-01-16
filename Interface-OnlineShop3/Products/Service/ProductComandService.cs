using Interface_OnlineShop3.Orders.Exceptions;
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
    public class ProductComandService : IProductComandService
    {
        private IProductRepository _productRepository;

        public ProductComandService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int RemoveProduct(int id)
        {
            if (_productRepository.Remove(id) != null)
            {
                return id;
            }
            else
            {
                throw new ProductNotFoundException();
            }
        }

        public Product AddProduct(Product productAdd)
        {
            if (_productRepository.Add(productAdd) != null)
            {
                 return productAdd;
            }
            else
            {
                throw new NullProductException();
            }
        }

        public Product UpdateProduct(int id, Product newProduct)
        {
            if(_productRepository.Add(newProduct) != null)
            {
                return newProduct;
            }
            else
            {
                throw new NullOrderException();
            }
        }
    }
}
