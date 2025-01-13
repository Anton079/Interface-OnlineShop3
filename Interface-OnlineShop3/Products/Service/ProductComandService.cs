using Interface_OnlineShop3.Orders.Exceptions;
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
            try
            {
                _productRepository.Remove(id);
                return id;
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return -1;
        }

        public Product AddProduct(Product productAdd)
        {
            try
            {
                _productRepository.Add(productAdd);
                return productAdd;
            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Product UpdateProduct(int id, Product newProduct)
        {
            try
            {
                _productRepository.Add(newProduct);
                return newProduct;
            }
            catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
