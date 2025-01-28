using Interface_OnlineShop3.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Models
{
    public class Admin : User
    {
        public Admin(int id, string type, string userName, string fullName, string email, string password, string billingAddress) :
            base(id, type, userName, fullName, email, password, billingAddress)
        {

        }

        public Admin(string proprietati) : base(proprietati)
        {
            string[] token = proprietati.Split(',');
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object? obj)
        {
            Admin admin = obj as Admin;
            return Id == admin.Id;
        }

        public override string ToSave()
        {
            return base.ToSave();
        }

    }
}
