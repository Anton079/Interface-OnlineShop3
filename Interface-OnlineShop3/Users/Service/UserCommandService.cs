using Interface_OnlineShop3.Orders.Exceptions;
using Interface_OnlineShop3.Users.Exceptions;
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
            if (user != null) throw new NullUserException();

            _userRepository.AddUser(user);
            return user;
        }

        public int RemoveUser(int id)
        {
            if(id == -1) throw new UserNotFoundException();

            _userRepository.Remove(id);
            return id;
        }

        public User UpdateUser(int id, User user)
        {
            if (id != -1) throw new UserNotFoundException();

            _userRepository.UpdateUser(id, user);
            return user;
        }
    }
}
