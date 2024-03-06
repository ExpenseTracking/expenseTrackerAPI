using Dapper;
using MySql.Data.MySqlClient;
using expenseTrackerAPI.Models.Expense;
using expenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM expenses WHERE isDeleted = 0";
                return conn.Query<Expense>(sql);
            }
        }

        public Expense GetExpenseById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM expenses WHERE expenseId = @id and isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return conn.QuerySingle<Expense>(sql, parameters);
            }
        }

        public int CreateExpense(Expense expense)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //                VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //                SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                                VALUES(@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted);
                                SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", expense.userId);
                parameters.Add("@transactionTypeId", expense.transactionTypeId);
                parameters.Add("@amount", expense.amount);
                parameters.Add("@date", expense.date);
                parameters.Add("@description", expense.description);
                parameters.Add("@createdAt", DateTime.UtcNow);
                parameters.Add("@updatedAt", DateTime.UtcNow);
                parameters.Add("@deletedAt", null);
                parameters.Add("@isDeleted", 0);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateExpense(Expense expense)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $@"UPDATE expenses 
                                SET userId = @userId, transactionTypeId = @transactionTypeId, amount = @amount, date = @date, description = @description, updatedAt = @updatedAt
                                WHERE expenseId = @expenseId;";

                var parameters = new DynamicParameters();
                parameters.Add("@expenseId", expense.expenseId);
                parameters.Add("@userId", expense.userId);
                parameters.Add("@transactionTypeId", expense.transactionTypeId);
                parameters.Add("@amount", expense.amount);
                parameters.Add("@date", expense.date);
                parameters.Add("@description", expense.description);
                parameters.Add("@updatedAt", DateTime.UtcNow);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteExpense(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = $@"UPDATE expenses
                                SET isDeleted = 1
                                WHERE expenseId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
