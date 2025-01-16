using Interface_OnlineShop3.Admins.Extensions;
using Interface_OnlineShop3.Admins.Models;
using Interface_OnlineShop3.Admins.Repository;
using Interface_OnlineShop3.Orders.Exceptions;
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
            try
            {
                _adminRepository.AddAdmin(admin);
                return admin;
            }catch (NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int RemoveAdmin(int id)
        {
            if(_adminRepository.Remove(id) != null)
            {
                return id;
            }
            else
            {
                throw new AdminNotFoundException();
            }
        }


        public Admin UpdateAdmin(int id, Admin admin)
        {
            if (_adminRepository.UpdateAdmin(id, admin) != null)
            {
                return admin;
            }
            else
            {
                throw new NullAdminException();
            }
        }
    }
}
