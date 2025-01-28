using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Models
{
    public class Customer : User
    {
        public Customer(int id, string type, string fullName, string userName, string email, string password, string billingAddress) : base(id, type, userName, fullName, email, password, billingAddress)
        {

        }

        public Customer(string proprietati) : base(proprietati)
        {
            string[] token = proprietati.Split(',');
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object? obj)
        {
            Customer customer = obj as Customer;
            return Id == customer.Id;
        }

        public override string ToSave()
        {
            return base.ToSave();
        }
    }
}
