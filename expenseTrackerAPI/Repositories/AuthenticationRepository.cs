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

        // public bool AuthenticateUser(User user)
        // {
        //     using (var conn = new SqlConnection(_connectionString))
        //     {
        //         string sql = "SELECT count(*) AS match FROM users WHERE username = @username AND password = @password AND isDeleted = 0";
        //         var parameters = new DynamicParameters();
        //         parameters.Add("@username", user.Username, DbType.String);
        //         parameters.Add("@password", user.Password, DbType.String);

        //         int match = (int)conn.ExecuteScalar(sql, parameters);

        //         return match == 1;
        //     }
        // }

        // public User GetAuthenticatedUserId(User user) 
        // {
        //     using (var conn = new SqlConnection(_connectionString))
        //     {
        //         string sql = "SELECT * FROM users WHERE username = @username AND password = @password AND isDeleted = 0";
        //         var parameters = new DynamicParameters();
        //         parameters.Add("@username", user.Username, DbType.String);
        //         parameters.Add("@password", user.Password, DbType.String);

        //         return conn.QuerySingle<User>(sql, parameters);
        //     }
        // }

        private readonly List<User> _users = new List<User>
        {
            new User { UserId = 1, Username = "user1", Password = "password1", IsDeleted = false },
            new User { UserId = 2, Username = "user2", Password = "password2", IsDeleted = false }
            // Add more users as needed
        };

        public bool AuthenticateUser(User user)
        {
            // Simulate authentication by checking if the username and password match
            int matchCount = _users.Count(u => u.Username == user.Username && u.Password == user.Password && u.IsDeleted == false);
            return matchCount == 1;
        }

        public User GetAuthenticatedUser(User user)
        {
            // Simulate retrieving user details after successful authentication
            User authenticatedUser = _users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password && u.IsDeleted == false);
            return authenticatedUser;
        }
    }
}