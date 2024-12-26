using Interface_OnlineShop3.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Service
{
    public interface IUserQueryService
    {
        List<User> GetAllUsers();

        User FindUserById(int id);
    }
}
