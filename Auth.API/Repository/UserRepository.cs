using Auth.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Repository
{
    public class UserRepository
    {
        public User Login(string email, string password)
        {
            if (email == "adm@adm.com" && password == "123")
                return new User() { Email = "adm@adm.com", Name = "John", Id = 1, Role = "ADM" };
            
            if (email == "user@user.com" && password == "123")
                return new User() { Email = "user@user.com", Name = "Alex", Id = 2, Role = "USR" };

            return new User();
        }

        public List<User> List()
        {
            List<User> list = new List<User>();
            list.Add(new User() { Email = "adm@adm.com", Name = "John", Id = 1, Role = "ADM" });
            list.Add(new User() { Email = "user@user.com", Name = "Alex", Id = 2, Role = "USR" });
            return list;
        }

        public User Get(int id)
        {
            return new User() { Email = "user@user.com", Name = "Alex", Id = 2, Role = "USR" };
        }
    }
}
