using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Service
{
    public interface IProductQueryService
    {
        List<Product> getAll();

        Product FindProductById(int id);

        int FindProductIdByName(string name);

        int FindProductStockByName(string name);

        int FindProductPriceByName(string name);

        public string FindProductNameById(int idProduct);

        List<Product> FindProductsByName(string searchTerm);

        List<Product> FindMinAndMaxProductsPrice(int minPrice, int maxPrice);
    }
}
