using Interface_OnlineShop3.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Models
{
    public class User : Customer
    {
        public User(int id, string fullName, string userName,string email, string password, string billingAddress) : base ( id, userName, fullName, email,  password, billingAddress)
        {

        }

        public User(string proprietati) : base (proprietati)
        {
            string[] token = proprietati.Split(',');
        }

        public override string ToSave()
        {
            return base.ToSave();
        }

        //tema
        //termin cu users, unde ii mai adaug 2 proprietati, username si password
        //fac admin si ii fac mostenire de la users cu el
        //si sa fac sistemul de login full
    }
}
