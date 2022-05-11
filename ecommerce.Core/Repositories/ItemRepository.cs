using System.Collections.Generic;
using System.Threading.Tasks;
using ecommerce.Core.Contexts;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.Sqlite;
using Dapper;
using ecommerce.Core.Database;


namespace ecommerce.Core.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbConfig _config;

        public ItemRepository(DbConfig config)
        {
            _config = config;
        }
        
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                return await connection.QueryAsync<Item>(@"
                SELECT id as Id, type as Type, rarity as Rarity, owner_id as OwnerId 
                FROM Items;");
            }
        }

        public async Task<Item> GetByIdAsync(long id)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                var item = await connection.QueryAsync<Item>(@"
                SELECT id as Id, type as Type, rarity as Rarity, owner_id as OwnerId 
                FROM Items
                WHERE id=@Id;",
                    new {Id = id}); 
                var resItem = item.FirstOrDefault();
                return resItem;
            }
        }

        public async Task AddAsync(Item entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                await connection.ExecuteAsync(@"
                INSERT INTO Items(id, type, rarity, owner_id)
                VALUES(@Id, @Type, @Rarity, @OwnerId);", 
                        entity);
            }
        }

        public async Task Update(Item entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                await connection.ExecuteAsync(@"
                UPDATE Items
                SET type=@Type, rarity=@Rarity, owner_id=@OwnerId
                WHERE id=@Id;",
                    entity);
            }
        }

        public async Task Delete(Item entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                await connection.ExecuteAsync(@"
                DELETE FROM Items
                WHERE id=@Id;", 
                        entity);                
            }
        }
    }
}