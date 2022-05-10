using System.Collections.Generic;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class UserFactory
    {
        public static User Create(string username)
        {
            var user = new User();
            user.Id = GioId.New();
            user.Username = username;
            user.Experience = 0;
            user.Wallet = 0;
            user.Bank = 0;
            user.Items = new List<Item>();
            return user;
        }
    }
}