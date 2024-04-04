using Dapper;
using expenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class GoalsRepository : IGoalsRepository
    {
        private readonly string _connectionString;

        public GoalsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Goals> GetGoals()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT * FROM goals 
                                WHERE isDeleted = 0";
                return conn.Query<Goals>(sql);
            }
        }

        public IEnumerable<Goals> GetGoalsByUserId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT * FROM goals
                                WHERE userId = @id AND isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.Query<Goals>(sql, parameters);
            }
        }

        public int CreateGoal(Goals goal)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO goals (userId, description, date, deadline, isCompleted, createdAt, updatedAt, deletedAt, isDeleted)
                                 VALUES(@userId, @description, @date, @deadline, @isCompleted, @createdAt, @updatedAt, @deletedAt, @isDeleted);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", goal.UserId, DbType.Int16);
                parameters.Add("@description", goal.Description, DbType.String);
                parameters.Add("@date", goal.Date, DbType.DateTime);
                parameters.Add("@deadline", goal.Deadline, DbType.DateTime);
                parameters.Add("@isCompleted", 0, DbType.Boolean);
                parameters.Add("@createdAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@deletedAt", null, DbType.DateTime);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateGoal(Goals goal)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE goals
                                SET description = @description, date = @date, deadline = @deadline, isCompleted = @isCompleted, updatedAt = @updatedAt
                                WHERE goalId = @goalId;";

                var parameters = new DynamicParameters();
                parameters.Add("@goalId", goal.GoalId, DbType.Int16);
                parameters.Add("@description", goal.Description, DbType.String);
                parameters.Add("@date", goal.Date, DbType.DateTime);
                parameters.Add("@deadline", goal.Deadline, DbType.DateTime);
                parameters.Add("@isCompleted", goal.IsCompleted, DbType.Boolean);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteGoal(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE goals
                                SET isDeleted = 1
                                WHERE goalId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
