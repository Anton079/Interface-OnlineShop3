using Interface_OnlineShop3.Products.Models;
using Interface_OnlineShop3.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> userList;

        public UserRepository()
        {
            userList = new List<User>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GetFilePath()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        User user = new User(line);
                        userList.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user data: {ex.Message}");
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folder = Path.Combine(currentDirectory, "data");
            string file = Path.Combine(folder, "UserData.txt"); // File name for storing user data
            return file;
        }

        private void SaveData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetFilePath()))
                {
                    sw.WriteLine(ToSaveAll());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user data: {ex.Message}");
            }
        }

        public string ToSaveAll()
        {
            string save = "";
            for (int i = 0; i < userList.Count; i++)
            {
                save += userList[i].ToSave();
                if (i < userList.Count - 1)
                {
                    save += "\n";
                }
            }
            return save;
        }

        // CRUD Operations

        public List<User> GetAll()
        {
            return userList;
        }

        public User AddUser(User user)
        {
            userList.Add(user);
            SaveData();
            return user;
        }

        public User Remove(int id)
        {
            User user = FindById(id);

            userList.Remove(user);
            SaveData();
            return user;
        }

        public User FindById(int id)
        {
            foreach (User user in userList)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User UpdateUser(int id, User user)
        {
            User userToUpdate = FindById(id);

            userToUpdate.FullName = user.FullName;
            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.BillingAddress = user.BillingAddress;

            SaveData();
            return userToUpdate;
        }

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(0, 1000000);

            while (FindById(id) != null)
            {
                id = rand.Next(0, 1000000);
            }
            return id;
        }
    }
}
