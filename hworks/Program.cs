using System;
using System.Collections.Generic;

namespace UserManagementSystem
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class UserManager
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public void UpdateUser(User user, string name, string email, string role)
        {
            user.Name = name;
            user.Email = email;
            user.Role = role;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UserManager manager = new UserManager();

            User user = new User
            {
                Name = "Azat",
                Email = "azt.abbsv@mail.ru",
                Role = "User"
            };

            manager.AddUser(user);
            manager.UpdateUser(user, "Azat Abbasov", "azt.abbsv@mail.ru", "Admin");
            manager.RemoveUser(user);
        }
    }
}


