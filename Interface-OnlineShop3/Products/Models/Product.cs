using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Products.Models
{
    public class Product
    {
        private int _id;
        private string _name;
        private int _price;
        private string _descriptions;
        private int _createDate;
        private int _stock;

        public Product(string proprietati)
        {
            string[] tokne = proprietati.Split(',');

            _id = int.Parse(tokne[0]);
            _name = tokne[1];
            _price = int.Parse(tokne[2]);
            _descriptions = tokne[3];
            _createDate = int.Parse(tokne[4]);
            _stock = int.Parse(tokne[5]);
        }

        public Product(int id, string name, int price, string descriptions, int createDate, int stock)
        {
            _id = id;
            _name = name;
            _price = price;
            _descriptions = descriptions;
            _createDate = createDate;
            _stock = stock;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Descriptions
        {
            get { return _descriptions; }
            set { _descriptions = value; }
        }

        public int CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }


        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public override string ToString()
        {
            return $"{Id},{Name},{Price},{Descriptions},{CreateDate},{Stock}";
        }

        public override bool Equals(object? obj)
        {
            Product products = obj as Product;
            return _id == products._id;
        }

        public string ToSave()
        {
            return Id + "," + Name + "," + Price + "," + Descriptions + "," + CreateDate + "," + Stock;
        }
    }
}
