using Interface_OnlineShop3.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Service
{
    namespace Interface_OnlineShop3.Users.Service
    {
        public interface IUserCommandService
        {
            User AddUser(User user);

            int RemoveUser(int id);

            User UpdateUser(int id, User user);
        }
    }
}
