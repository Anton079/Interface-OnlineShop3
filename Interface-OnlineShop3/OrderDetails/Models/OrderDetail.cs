using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.OrderDetails.Models
{
    public class OrderDetail
    {
        private int _id;
        private int _orderId;
        private int _productId;
        private int _price;
        private int _quantity;

        public OrderDetail(string proprietati)
        {
            string[] tokne = proprietati.Split(',');

            _id = int.Parse(tokne[0]);
            _orderId = int.Parse(tokne[1]);
            _productId = int.Parse(tokne[2]);
            _price = int.Parse(tokne[3]);
            _quantity = int.Parse(tokne[4]);
        }

        public OrderDetail(int id, int orderId, int productId, int price, int quantity)
        {
            _id = id;
            _orderId = orderId;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Id},{OrderId},{ProductId},{Price},{Quantity}";
        }

        public override bool Equals(object? obj)
        {
            OrderDetail orderDetails = obj as OrderDetail;
            return _id == orderDetails._id;
        }

        public string ToSave()
        {
            return Id + "," + OrderId + "," + ProductId + "," + Price + "," + Quantity;
        }
    }

}
