using Dapper;
using MySql.Data.MySqlClient;
using expenseTrackerAPI.Models;


namespace expenseTrackerAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetUsers()
        {   
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM users WHERE isDeleted = 0";
                return conn.Query<User>(sql);
            }
        }

        public User GetUserById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM users WHERE userId = @id and isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return conn.QuerySingle<User>(sql, parameters);
            }
        }

        public int CreateUser(User user)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $@"INSERT INTO users (username, password, email, roleId, createdAt, updatedAt, deletedAt, isDeleted)
                                VALUES (@username, @password, @email, @roleId, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                                SELECT LAST_INSERT_ID();";

                // query for sql server
                // string sql = $@"INSERT INTO users (username, password, email, roleId, createdAt, updatedAt, deletedAt, isDeleted)
                //                 VALUES (@username, @password, @email, @roleId, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //                 SELECT SCOPE_IDENTITY();";
                var parameters = new DynamicParameters();
                parameters.Add("@username", user.username);
                parameters.Add("@password", user.password);
                parameters.Add("@email", user.email);
                parameters.Add("@roleId", user.roleId);
                parameters.Add("@createdAt", DateTime.UtcNow);
                parameters.Add("@updatedAt", DateTime.UtcNow);
                parameters.Add("@deletedAt", null);
                parameters.Add("@isDeleted", 0);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateUser(User user)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $@"UPDATE users 
                                SET username = @username, password = @password, email = @email, roleId = @roleId, updatedAt = @updatedAt
                                WHERE userId = @userId;";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", user.userId);
                parameters.Add("@username", user.username);
                parameters.Add("@password", user.password);
                parameters.Add("@email", user.email);
                parameters.Add("@roleId", user.roleId);
                parameters.Add("@updatedAt", DateTime.UtcNow);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteUser(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $@"UPDATE users
                                SET isDeleted = 1
                                WHERE userId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
