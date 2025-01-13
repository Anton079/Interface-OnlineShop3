using Interface_OnlineShop3.Admins.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Admins.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private List<Admin> adminList;

        public AdminRepository()
        {
            adminList = new List<Admin>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GetFilePath()))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        Admin admin = new Admin(line);
                        adminList.Add(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string folder = Path.Combine(currentDirectory, "data");
            string file = Path.Combine(folder, "Admin");
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
                Console.WriteLine(ex.Message);
            }
        }

        public string ToSaveAll()
        {
            string save = "";
            for (int i = 0; i < adminList.Count; i++)
            {
                save += adminList[i].ToSave();
                if (i < adminList.Count - 1)
                {
                    save += "\n";
                }
            }
            return save;
        }

        // CRUD

        public List<Admin> GetAll()
        {
            return adminList;
        }

        public Admin AddAdmin(Admin admin)
        {
            adminList.Add(admin);
            SaveData();
            return admin;
        }

        public Admin Remove(int id)
        {
            Admin admin = FindById(id);

            adminList.Remove(admin);
            SaveData();
            return admin;
        }

        public Admin FindById(int id)
        {
            if(id != -1)
            {
                foreach (Admin admin in adminList)
                {
                    if (admin.Id == id)
                    {
                        return admin;
                    }
                }
            }
            return null;
        }

        public Admin UpdateAdmin(int id, Admin admin)
        {
            Admin adminUpdate = FindById(id);

            adminUpdate.FullName = admin.FullName;
            adminUpdate.Password = admin.Password;
            adminUpdate.BillingAddress = admin.BillingAddress;

            SaveData();
            return adminUpdate;
        }

        public int GenerateId()
        {
            Random rand = new Random();

            int id = rand.Next(0, 1000000);

            while (FindById(id) != null)
            {
                id = rand.Next(0, 100000);
            }
            return id;
        }
    }
}
