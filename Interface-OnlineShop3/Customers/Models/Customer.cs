using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Customers.Models
{
    public class Customer
    {
        private int _id;
        private string _userName;
        private string _fullName;
        private string _email;
        private string _password;
        private string _billingAddress;

        public Customer(string proprietati)
        {
            string[] token = proprietati.Split(',');

            _id = int.Parse(token[0]);
            _userName = token[1];
            _fullName = token[2];
            _email = token[3];
            _password = token[4];
            _billingAddress = token[5];
        }

        public Customer(int id, string userName, string fullName, string email, string password, string billingAddress)
        {
            _id = id;
            _userName = userName;
            _fullName = fullName;
            _email = email;
            _password = password;
            _billingAddress = billingAddress;
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string BillingAddress
        {
            get { return _billingAddress; }
            set { _billingAddress = value; }
        }

        public override string ToString()
        {
            return $"{Id},{UserName},{FullName},{Email},{Password},{BillingAddress}";
        }

        public override bool Equals(object? obj)
        {
            Customer customers = obj as Customer;
            return _id == customers._id;
        }

        public virtual string ToSave()
        {
            return Id + "," + UserName + "," + FullName + "," + Email + "," + Password + "," + BillingAddress;
        }
    }
}
