using Interface_OnlineShop3.Admins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Admins.Repository
{
    public interface IAdminRepository
    {
        List<Admin> GetAll();

        Admin AddAdmin(Admin admin);

        Admin Remove(int id);

        Admin FindById(int id);

        Admin UpdateAdmin(int id, Admin admin);

        int GenerateId();
    }
}
