using Interface_OnlineShop3.Users.Models;
using Interface_OnlineShop3.Users.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Service
{
    public class UserQueryService : IUserQueryService
    {
        private IUserRepository _userRepository;

        public UserQueryService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User FindUserById(int id)
        {
            if (id != -1)
            {
                return _userRepository.FindById(id);
            }
            return null;
        }
    }
}
