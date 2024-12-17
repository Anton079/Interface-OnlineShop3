using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.System.DTOs
{
    public class OrderDetailsDto
    {

        private int _productId;
        private string _productName;
        private int _quantity;
        private int _price;

        public OrderDetailsDto(int productId, string productName, int quantity, int price)
        {
            _productId = productId;
            _productName = productName;
            _quantity = quantity;
            _price = price;
        }

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

    }
}
