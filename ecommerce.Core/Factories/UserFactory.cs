using System.Collections.Generic;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class UserFactory
    {
        public static User Create(string username)
        {
            var user = new User()
            {
                Id = GioId.New(),
                Username = username,
                Experience = 0,
                Wallet = 0,
                Bank = 0,
                Items = new List<Item>(),    
            };

            return user;
        }
    }
}