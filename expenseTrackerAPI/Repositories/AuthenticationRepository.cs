using Dapper;
using expenseTrackerAPI.Models.User;
using System.Data;
using Microsoft.Data.SqlClient;


namespace expenseTrackerAPI.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _connectionString;

        public AuthenticationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AuthenticateUser(User user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT count(*) AS match FROM users WHERE username = @username AND password = @password AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@username", user.Username, DbType.String);
                parameters.Add("@password", user.Password, DbType.String);

                int match = (int)conn.ExecuteScalar(sql, parameters);

                return match == 1;
            }
        }

        public User GetAuthenticatedUser(User user) 
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM users WHERE username = @username AND password = @password AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@username", user.Username, DbType.String);
                parameters.Add("@password", user.Password, DbType.String);

                return conn.QuerySingle<User>(sql, parameters);
            }
        }
    }
}
