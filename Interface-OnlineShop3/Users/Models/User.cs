using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Models
{
    public class User
    {
        private int _id;
        private string _type;
        private string _userName;
        private string _fullName;
        private string _email;
        private string _password;
        private string _billingAddress;

        public User(string proprietati)
        {
            string[] token = proprietati.Split(',');

            _id = int.Parse(token[0]);
            _type = token[1];
            _userName = token[2];
            _fullName = token[3];
            _email = token[4];
            _password = token[5];
            _billingAddress = token[6];
        }

        public User(int id, string type, string userName, string fullName, string email, string password, string billingAddress)
        {
            _id = id;
            _type = type;
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

        public string Type
        {
            get { return _type; }
            set { _type = value; }
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
            return $"{Id},{Type},{UserName},{FullName},{Email},{Password},{BillingAddress}";
        }

        public override bool Equals(object? obj)
        {
            User user = obj as User;
            return _id == user._id;
        }

        public virtual string ToSave()
        {
            return Id + "," + Type + "," + UserName + "," + FullName + "," + Email + "," + Password + "," + BillingAddress;
        }
    }
}
