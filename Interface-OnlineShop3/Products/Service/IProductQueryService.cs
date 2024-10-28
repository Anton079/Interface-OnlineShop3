using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Products.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop.Products.Service
{
    public interface IProductQueryService 
    {
        List<Product> getAll();

        Product FindProductById(int id);
    }
}
