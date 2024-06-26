using Dapper;
using expenseTrackerAPI.Models.User;
using System.Data;
using Microsoft.Data.SqlClient;


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
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM users WHERE isDeleted = 0";
                return conn.Query<User>(sql);
            }
        }

        public IEnumerable<User> GetUserById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM users WHERE userId = @id AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.Query<User>(sql, parameters);
            }
        }

        public int CreateUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                //string sql = $@"INSERT INTO users (username, password, email, roleId, createdAt, updatedAt, deletedAt, isDeleted)
                //                VALUES (@username, @password, @email, @roleId, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //                SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO users (username, password, email, roleId, createdAt, updatedAt, deletedAt, isDeleted)
                                 VALUES (@username, @password, @email, @roleId, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                                 SELECT SCOPE_IDENTITY();";
                var parameters = new DynamicParameters();
                parameters.Add("@username", user.Username, DbType.String);
                parameters.Add("@password", user.Password, DbType.String);
                parameters.Add("@email", user.Email, DbType.String);
                parameters.Add("@roleId", user.RoleId, DbType.Int16);
                parameters.Add("@createdAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@deletedAt", null, DbType.DateTime);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE users 
                                SET username = @username, password = @password, email = @email, roleId = @roleId, updatedAt = @updatedAt
                                WHERE userId = @userId;";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", user.UserId, DbType.Int16);
                parameters.Add("@username", user.Username, DbType.String);
                parameters.Add("@password", user.Password, DbType.String);
                parameters.Add("@email", user.Email, DbType.String);
                parameters.Add("@roleId", user.RoleId, DbType.Int16);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteUser(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE users
                                SET isDeleted = 1, deletedAt = @deletedAt
                                WHERE userId = @id AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);
                parameters.Add("@deletedAt", DateTime.UtcNow, DbType.DateTime);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
