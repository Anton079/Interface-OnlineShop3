using Interface_OnlineShop3.Admins.Models;
using Interface_OnlineShop3.Admins.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Admins.Service
{
    public class AdminCommandService : IAdminCommandService
    {
        private IAdminRepository _adminRepository;

        public AdminCommandService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin AddAdmin(Admin admin)
        {
            if (admin != null)
            {
                _adminRepository.AddAdmin(admin);
                return admin;
            }
            return null;
        }

        public int RemoveAdmin(int id)
        {
            if (id != -1)
            {
                _adminRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public Admin UpdateAdmin(int id, Admin admin)
        {
            if (id != -1 && admin != null)
            {
                _adminRepository.UpdateAdmin(id, admin);
                return admin;
            }
            return null;
        }
    }
}
