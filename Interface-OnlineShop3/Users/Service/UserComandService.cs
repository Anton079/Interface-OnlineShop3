using Interface_OnlineShop3.Orders.Exceptions;
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
            try
            {
                _userRepository.AddUser(user);
                return user;
            }catch(NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int RemoveUser(int id)
        {
            try
            {
                _userRepository.Remove(id);
                return id;
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return -1;
        }

        public User UpdateUser(int id, User user)
        {
            try
            {
                if(id != -1)
                {
                    throw new OrderNotFoundException();
                }

                _userRepository.UpdateUser(id, user);
                return user;
            }catch(NullOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
