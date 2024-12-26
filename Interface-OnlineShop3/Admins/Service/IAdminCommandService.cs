using Interface_OnlineShop3.Admins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Admins.Service
{
    public interface IAdminCommandService
    {
        Admin AddAdmin(Admin admin);

        int RemoveAdmin(int id);

        Admin UpdateAdmin(int id, Admin admin);
    }
}
