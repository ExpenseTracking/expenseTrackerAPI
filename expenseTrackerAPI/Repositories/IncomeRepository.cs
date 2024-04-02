using Dapper;
using expenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly string _connectionString;

        public IncomeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Income> GetIncome()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT * FROM income 
                                WHERE isDeleted = 0";
                return conn.Query<Income>(sql);
            }
        }

        public IEnumerable<Income> GetIncomeByUserId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT i.*
	                                   ,s.incomeSourceName
	                                   ,u.username
                                FROM income i
                                JOIN incomeSource s ON i.incomeSourceId = s.incomeSourceId
                                JOIN users u ON i.userId = u.userId
                                WHERE i.userId = @userId AND i.isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@userId", id, DbType.Int16);

                return conn.Query<Income>(sql, parameters);
            }
        }

        public int CreateIncome(Income income)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO income (userId, incomeSourceId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                                 VALUES(@userId, @incomeSourceId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", income.UserId, DbType.Int16);
                parameters.Add("@incomeSourceId", income.IncomeSourceId, DbType.Int16);
                parameters.Add("@amount", income.Amount, DbType.Decimal);
                parameters.Add("@date", income.Date, DbType.DateTime);
                parameters.Add("@description", income.Description, DbType.String);
                parameters.Add("@createdAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);
                parameters.Add("@deletedAt", null, DbType.DateTime);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateIncome(Income income)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE income 
                                SET incomeSourceId = @incomeSourceId, amount = @amount, date = @date, description = @description, updatedAt = @updatedAt
                                WHERE incomeId = @incomeId;";

                var parameters = new DynamicParameters();
                parameters.Add("@incomeId", income.IncomeId, DbType.Int16);
                parameters.Add("@incomeSourceId", income.IncomeSourceId, DbType.Int16);
                parameters.Add("@amount", income.Amount, DbType.Decimal);
                parameters.Add("@date", income.Date, DbType.DateTime);
                parameters.Add("@description", income.Description, DbType.String);
                parameters.Add("@updatedAt", DateTime.UtcNow, DbType.DateTime);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteIncome(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE income
                                SET isDeleted = 1
                                WHERE incomeId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
