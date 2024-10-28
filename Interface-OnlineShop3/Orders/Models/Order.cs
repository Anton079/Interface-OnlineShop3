using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Orders.Models
{
    public class Order
    {
        private int _id;
        private int _customerId;
        private int _amount;
        private string _shippingAddress;

        public Order(string proprietati)
        {
            string[] tokne = proprietati.Split(',');

            _id = int.Parse(tokne[0]);
            _customerId = int.Parse(tokne[1]);
            _amount = int.Parse(tokne[2]);
            _shippingAddress = tokne[3];
        }

        public Order(int id, int customerId, int amount, string shippingAddress)
        {
            _id = id;
            _customerId = customerId;
            _amount = amount;
            _shippingAddress = shippingAddress;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string ShippingAddress
        {
            get { return _shippingAddress; }
            set { _shippingAddress = value; }
        }

        public override string ToString()
        {
            return $"{Id},{CustomerId},{Amount},{ShippingAddress}";
        }

        public override bool Equals(object? obj)
        {
            Order orders = obj as Order;
            return _id == orders._id;
        }

        public string ToSave()
        {
            return Id + "," + CustomerId + "," + Amount + "," + ShippingAddress;
        }
    }
}
