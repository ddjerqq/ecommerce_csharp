using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ecommerce.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;

        public UserService(IUserRepository userRepository, IItemRepository itemRepository)
        {
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();

            foreach (User user in users)
            {
                var userItems = 
                    from item in items
                    where item.OwnerId == user.Id
                    select item;
                
                user.Items = userItems.ToList();
            }

            return users;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var items = await _itemRepository.GetAllAsync();
            
            if (items.Count() != 0)
            {
                var userItems =
                    from item in items
                    where item.OwnerId == user.Id
                    select item;
                
                user.Items = userItems.ToList();
            }
            else
            {
                user.Items = new List<Item>();
            }
            
            return user;
        }

        public async Task AddAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            await _userRepository.Update(entity);
            
            var dbItems = await _itemRepository.GetAllAsync();

            var allItems = dbItems.ToHashSet();
            allItems.UnionWith(entity.Items);

            foreach (Item item in allItems)
            {
                if (entity.Items.Contains(item) && !dbItems.Contains(item))
                {
                    await _itemRepository.AddAsync(item);
                } 
                else if (!entity.Items.Contains(item) && dbItems.Contains(item))
                {
                    await _itemRepository.Delete(item);
                }
                else
                {
                    await _itemRepository.Update(item);
                }
            }
        }

        public async Task DeleteAsync(User entity)
        {
            await _userRepository.Delete(entity);
            foreach (Item item in entity.Items)
            {
                await _itemRepository.Delete(item);
            }
        }
    }
}