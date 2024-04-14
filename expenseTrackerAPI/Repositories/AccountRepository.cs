using Dapper;
using expenseTrackerAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using expenseTrackerAPI.Models.User;


namespace expenseTrackerAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Account> GetAccounts()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM users WHERE isDeleted = 0";
                return conn.Query<Account>(sql);
            }
        }

        public int GetUserId(Account account)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT userId FROM users 
                                WHERE username = @username AND password = @password AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@username", account.Username, DbType.String);
                parameters.Add("@password", account.Password, DbType.String);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public int CreateUser(Account account)
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
                parameters.Add("@username", account.Username, DbType.String);
                parameters.Add("@password", account.Password, DbType.String);
                parameters.Add("@email", account.Email, DbType.String);
                parameters.Add("@roleId", account.RoleId, DbType.Int16);
                parameters.Add("@createdAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@deletedAt", null, DbType.DateTime);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateUser(Account account)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE users 
                                SET username = @username, password = @password, email = @email, roleId = @roleId, updatedAt = @updatedAt
                                WHERE userId = @userId;";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", account.UserId, DbType.Int16);
                parameters.Add("@username", account.Username, DbType.String);
                parameters.Add("@password", account.Password, DbType.String);
                parameters.Add("@email", account.Email, DbType.String);
                parameters.Add("@roleId", account.RoleId, DbType.Int16);
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
