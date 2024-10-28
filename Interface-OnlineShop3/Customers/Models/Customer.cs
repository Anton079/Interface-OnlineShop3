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
        private string _email;
        private string _password;
        private string _fullName;
        private string _billingAddress;

        public Customer(string proprietati)
        {
            string[] tokne = proprietati.Split(',');

            _id = int.Parse(tokne[0]);
            _email = tokne[1];
            _password = tokne[2];
            _fullName = tokne[3];
            _billingAddress = tokne[4];

        }

        public Customer(int id, string email, string password, string fullName, string billingAddress)
        {
            _id = id;
            _email = email;
            _password = password;
            _fullName = fullName;
            _billingAddress = billingAddress;

        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
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

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string BillingAddress
        {
            get { return _billingAddress; }
            set { _billingAddress = value; }
        }

        public override string ToString()
        {
            return $"{Id},{Email},{Password},{FullName},{BillingAddress}";
        }

        public override bool Equals(object? obj)
        {
            Customer customers = obj as Customer;
            return _id == customers._id;
        }

        public string ToSave()
        {
            return Id + "," + Email + "," + Password + "," + FullName + "," + BillingAddress;
        }
    }
}
