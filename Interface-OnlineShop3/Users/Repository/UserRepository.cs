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
                string filePath = GetFilePath();
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Fisierul {filePath} nu exista");
                    return;
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string type = line.Split(',')[0];

                        switch (type)
                        {
                            case "Admin": userList.Add(new Admin(line)); break;
                            case "Customer": userList.Add(new Customer(line)); break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string folder = Path.Combine(currentDirectory, "data");

            string file = Path.Combine(folder, "User");

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
                Console.WriteLine(ex);
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

            if(userToUpdate is Customer customerToUpdate && user is Customer customer)
            {
                userToUpdate.FullName = customer.FullName;
                userToUpdate.UserName = customer.UserName;
                userToUpdate.Email = customer.Email;
                userToUpdate.Password = customer.Password;
                userToUpdate.BillingAddress = customer.BillingAddress;
            }else if(userToUpdate is Admin adminToUpdate && user is Admin admin)
            {
                adminToUpdate.FullName = admin.FullName;
                adminToUpdate.UserName = admin.UserName;
                adminToUpdate.Email = admin.Email;
                adminToUpdate.Password = admin.Password;
                adminToUpdate.BillingAddress = admin.BillingAddress;
            }

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
