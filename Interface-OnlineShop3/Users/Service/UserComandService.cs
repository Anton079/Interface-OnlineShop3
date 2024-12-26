using Interface_OnlineShop3.Users.Models;
using Interface_OnlineShop3.Users.Repository;
using Interface_OnlineShop3.Users.Service.Interface_OnlineShop3.Users.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Service
{
    public class UserCommandService : IUserCommandService
    {
        private IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User AddUser(User user)
        {
            if (user != null)
            {
                _userRepository.AddUser(user);
                return user;
            }
            return null;
        }

        public int RemoveUser(int id)
        {
            if (id != -1)
            {
                _userRepository.Remove(id);
                return id;
            }
            return -1;
        }

        public User UpdateUser(int id, User user)
        {
            if (id != -1 && user != null)
            {
                _userRepository.UpdateUser(id, user);
                return user;
            }
            return null;
        }
    }
}
