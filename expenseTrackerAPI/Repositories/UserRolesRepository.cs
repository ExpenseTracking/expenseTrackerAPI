using Dapper;
using expenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly string _connectionString;

        public UserRolesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<UserRoles> GetUserRoles()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM userRoles";
                return conn.Query<UserRoles>(sql);
            }
        }

        public UserRoles GetUserRoleById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM userRoles WHERE roleId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.QuerySingle<UserRoles>(sql, parameters);
            }
        }

        public int CreateUserRole(UserRoles userRole)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO userRoles (roleName)
                                 VALUES(@roleName);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                //parameters.Add("@roleId", userRole.RoleId, DbType.Int16);
                parameters.Add("@roleName", userRole.RoleName, DbType.String);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateUserRole(UserRoles userRole)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE userRoles 
                                SET roleName = @roleName
                                WHERE roleId = @roleId;";

                var parameters = new DynamicParameters();
                parameters.Add("@roleId", userRole.RoleId, DbType.Int16);
                parameters.Add("@roleName", userRole.RoleName, DbType.String);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteUserRole(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE userRoles
                                SET isDeleted = 1
                                WHERE roleId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
