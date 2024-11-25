using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Cos.DTOs
{
    public class OrderDetailsDto
    {

        private int _productId;
        private string _productName;
        private int _quantity;

        public OrderDetailsDto(int productId, string productName, int quantity)
        {
            _productId = productId;
            _productName = productName;
            _quantity = quantity;
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

    }
}
