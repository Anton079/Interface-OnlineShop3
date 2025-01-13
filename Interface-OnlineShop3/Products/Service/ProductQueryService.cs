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
            try
            {
                Product product = _prodRepository.FindById(id);

                return product;
            }catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int FindProductIdByName(string nameProduct)
        {
            try
            {
                Product product = _prodRepository.FindByName(nameProduct);
                return product.Id;
            }catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        public int FindProductStockByName(string nameProduct)
        {
            try
            {
                Product product = _prodRepository.FindByName(nameProduct);

                return product.Stock;
            }catch(OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public int FindProductPriceByName(string nameProduct)
        {
            try
            {
                Product product = _prodRepository.FindByName(nameProduct);

                return product.Price;
            }catch(OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
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
