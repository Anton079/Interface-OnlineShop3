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

            return product;
        }

        public int FindProductIdByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);

            return product.Id;
        }

        public int FindProductStockByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);

            return product.Stock;
        }

        public int FindProductPriceByName(string nameProduct)
        {
            Product product = _prodRepository.FindByName(nameProduct);

            return product.Price;
        }

        public string FindProductNameById(int idProduct)
        {
            if(idProduct != -1)
            {
                Product product = _prodRepository.FindById(idProduct);
                return product.Name;
            }
            else
            {
                Console.WriteLine("Id ul este gol!");
            }
            return null;
        }
    }
}
