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
    public class AdminQueryService : IAdminQueryService
    {
        private IAdminRepository _adminRepository;

        public AdminQueryService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<Admin> GetAllAdmins()
        {
            return _adminRepository.GetAll();
        }

        public Admin FindAdminById(int id)
        {
            Admin ad = _adminRepository.FindById(id);

            if(ad != null)
            {
                return ad;
            }
            else
            {
                throw new AdminNotFoundException(); 
            }
        }
    }
}
