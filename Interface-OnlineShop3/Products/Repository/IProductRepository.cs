using Interface_OnlineShop3.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interface_OnlineShop3.Products.Repository
{
    public interface IProductRepository
    {
        List<Product> getAll();

        Product Add(Product product);

        Product Remove(int id);

        Product FindById(int id);

        Product FindByName(string name);

        Product UpdateProducts(int id, Product update);

        int GenerateId();

        void SaveData();

    }
}
