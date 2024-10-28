using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Service
{
    public interface IProductComandService
    {
        Product AddProduct(Product product);

        int RemoveProduct(int id);

        Product UpdateProduct(int id, Product product);
    }
}
