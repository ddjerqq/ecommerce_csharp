using System.Threading.Tasks;
using Dapper;
using ecommerce.Core.Database;
using ecommerce.Core.Models;
using ecommerce.Core.Models.DataTransferObjects;
using ecommerce.Core.Services.Interfaces;
using Microsoft.Data.Sqlite;

namespace ecommerce.Core.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly DbConfig _dbConfig;
        
        public AnalyticsService(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        
        /// <summary>
        /// Get total profit.
        /// </summary>
        /// <returns>decimal</returns>
        public async Task<decimal> TotalProfit()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                // sum all the prices of all the products from other tables
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT SUM(P.Price * O.Quantity)
                    FROM Orders O
                    JOIN Products P on O.ProductId = P.Id
                ");
                return data;
            }
        }
        
        /// <summary>
        /// Get the most sold product type
        /// </summary>
        /// <returns>ProductTypeDto</returns>
        public async Task<ProductTypeDto> MostSoldProductType()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                // sum all the prices of all the products from other tables
                var data = await connection.QuerySingleAsync<ProductTypeDto>(@"
                    SELECT P.Name, P.Description, P.Price, P.Stock
                    FROM Products P 
                    JOIN Orders O ON P.Id = O.ProductId
                    ORDER BY (SELECT SUM(O.Quantity) 
                              FROM Orders O
                              JOIN Products P2 on O.ProductId = P2.Id) DESC 
                    LIMIT 1;
                ");
                return data;
            }
        }

        /// <summary>
        /// Get the top customer, with the most orders.
        /// </summary>
        /// <returns>Customer</returns>
        public async Task<Customer> TopCustomer()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                // sum all the prices of all the products from other tables
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT C.Id
                    FROM Customers C
                    JOIN Orders O ON C.Id = O.CustomerId
                    ORDER BY (SELECT COUNT(C2.Id) FROM Orders
                              JOIN Customers C2 on Orders.CustomerId = C2.Id) DESC
                    LIMIT 1;
                ");
                
                var topCustomer = await connection.QuerySingleAsync<Customer>(@"
                    SELECT *
                    FROM Customers
                    WHERE Id = @Id", new {Id = data});
                
                return topCustomer;
            }
        }
        
        /// <summary>
        /// Get total amount of orders.
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> TotalOrders()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT COUNT(Id)
                    FROM Orders
                ");
                return data;
            }
        }
        
        /// <summary>
        /// Get total amount of products.
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> TotalProducts()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT COUNT(Id)
                    FROM Products
                ");
                return data;
            }
        }

        /// <summary>
        /// Get total amount of customers registered.
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> TotalCustomers()
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT COUNT(Id)
                    FROM Customers
                ");
                return data;
            }
        }

        /// <summary>
        /// Get total sales by product.
        /// </summary>
        /// <param name="product">the product object</param>
        /// <returns>int - amount of products sold</returns>
        public async Task<int> TotalSalesByProduct(Product product)
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT SUM(O.Quantity)
                    FROM Orders O
                    JOIN Products P ON O.ProductId = P.Id
                    WHERE P.Id = @Id
                ", new {Id = product.Id});
                return data;
            }
        }

        /// <summary>
        /// Get total sales by customer.
        /// </summary>
        /// <param name="customer">the customer object</param>
        /// <returns>int - amount of sales</returns>
        public async Task<int> TotalSalesByCustomer(Customer customer)
        {
            using (SqliteConnection connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                int data = await connection.QuerySingleAsync<int>(@"
                    SELECT SUM(O.Quantity)
                    FROM Orders O
                    JOIN Customers C ON O.CustomerId = C.Id
                    WHERE C.Id = @Id
                ", new {Id = customer.Id});
                return data;
            }
        }
    }
}