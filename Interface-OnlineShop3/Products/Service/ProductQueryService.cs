using Interface_OnlineShop3.Products.Models;
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
        private IProductRepository _proQRepository;

        public ProductQueryService(IProductRepository proQRepository)
        {
            _proQRepository = proQRepository;
        }

        //crud

        public List<Product> getAll()
        {
            List<Product> products = new List<Product>();

            if(products.Count == 0)
            {
                return null;
            }
            return products;
        }

        public Product FindProductById(int id)
        {
            Product product = _proQRepository.FindById(id);

            return product;
        }
    }
}
