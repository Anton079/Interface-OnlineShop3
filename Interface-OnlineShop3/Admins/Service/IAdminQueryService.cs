using Interface_OnlineShop3.Admins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Admins.Service
{
    public interface IAdminQueryService
    {
        List<Admin> GetAllAdmins();

        Admin FindAdminById(int id);
    }
}
