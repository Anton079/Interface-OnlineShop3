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
            if (id != -1)
            {
                _productRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public Product AddProduct(Product productAdd)
        {
            if(productAdd != null && _productRepository.FindById(productAdd.Id) == null) 
            {
                _productRepository.Add(productAdd);
                return productAdd;
            }
            return null;
        }

        public Product UpdateProduct(int id,Product newProduct)
        {

            if(newProduct != null && _productRepository.FindById(id) != null)
            {
                _productRepository.Add(newProduct);
                return newProduct;
            }
            return null;
        }
    }
}
