namespace ecommerce.Core.Database
{
    public class DbConfig
    {
        private readonly string _connectionString;

        public DbConfig(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string ConnectionString { get => _connectionString; }
    }
}