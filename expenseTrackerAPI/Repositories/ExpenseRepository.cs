using Dapper;
using expenseTrackerAPI.Models.Expense;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string _connectionString;

        public ExpenseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Expense> GetExpenses()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM expenses WHERE isDeleted = 0";
                return conn.Query<Expense>(sql);
            }
        }

        public Expense GetExpenseById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM expenses WHERE expenseId = @id and isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.QuerySingle<Expense>(sql, parameters);
            }
        }

        public int CreateExpense(Expense expense)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                                 VALUES(@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", expense.UserId, DbType.Int16);
                parameters.Add("@transactionTypeId", expense.TransactionTypeId, DbType.Int16);
                parameters.Add("@amount", expense.Amount, DbType.Decimal);
                parameters.Add("@date", expense.Date, DbType.DateTime);
                parameters.Add("@description", expense.Description, DbType.String);
                parameters.Add("@createdAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@deletedAt", null, DbType.DateTime);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateExpense(Expense expense)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE expenses 
                                SET userId = @userId, transactionTypeId = @transactionTypeId, amount = @amount, date = @date, description = @description, updatedAt = @updatedAt
                                WHERE expenseId = @expenseId;";

                var parameters = new DynamicParameters();
                parameters.Add("@expenseId", expense.ExpenseId, DbType.Int16);
                parameters.Add("@userId", expense.UserId, DbType.Int16);
                parameters.Add("@transactionTypeId", expense.TransactionTypeId, DbType.Int16);
                parameters.Add("@amount", expense.Amount, DbType.Decimal);
                parameters.Add("@date", expense.Date, DbType.DateTime);
                parameters.Add("@description", expense.Description, DbType.String);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteExpense(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE expenses
                                SET isDeleted = 1
                                WHERE expenseId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
