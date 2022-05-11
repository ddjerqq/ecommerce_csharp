using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ecommerce.Core.Contexts;
using ecommerce.Core.Database;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConfig _config;

        public UserRepository(DbConfig config)
        {
            _config = config;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                return await connection.QueryAsync<User>(@"
                SELECT id as Id, username as Username, experience as Experience, wallet as Wallet, bank as Bank 
                FROM Users
                ");
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                var user = await connection.QueryAsync<User>(@"
                SELECT id as Id, username as Username, experience as Experience, wallet as Wallet, bank as Bank
                FROM Users
                WHERE id=@Id
                ", new {Id = id});
                
                var resUser = user.FirstOrDefault();
                
                return resUser;
            }
        }

        public async Task AddAsync(User entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                await connection.ExecuteAsync(@"
                INSERT INTO Users(id, username, experience, wallet, bank)
                VALUES(@Id, @Username, @Experience, @Wallet, @Bank);",
                    entity);
            }
        }

        public async Task Update(User entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                
                await connection.ExecuteAsync(@"
                UPDATE Users
                SET username=@Username, experience=@Experience, wallet=@Wallet, bank=@Bank
                WHERE id=@Id;",
                    entity);
            }
        }

        public async Task Delete(User entity)
        {
            using (var connection = new SqliteConnection(_config.ConnectionString))
            {
                await connection.ExecuteAsync(@"
                DELETE FROM Users
                WHERE id=@Id;",
                    entity);
            }
        }
    }
}