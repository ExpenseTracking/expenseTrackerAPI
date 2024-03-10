using Dapper;
using expenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        private readonly string _connectionString;

        public IncomeSourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<IncomeSource> GetIncomeSources()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM incomeSource WHERE isDeleted = 0";
                return conn.Query<IncomeSource>(sql);
            }
        }

        public IncomeSource GetIncomeSourceById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $"SELECT * FROM incomeSource WHERE incomeSourceId = @id and isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.QuerySingle<IncomeSource>(sql, parameters);
            }
        }

        public int CreateIncomeSource(IncomeSource incomeSource)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO incomeSource (userId, incomeSourceName, isDeleted)
                                 VALUES(@userId, @incomeSourceName, @isDeleted);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", incomeSource.UserId, DbType.Int16);
                parameters.Add("@incomeSourceName", incomeSource.IncomeSourceName, DbType.String);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateIncomeSource(IncomeSource incomeSource)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE incomeSource 
                                SET userId = @userId, incomeSourceName = @incomeSourceName
                                WHERE incomeSourceId = @incomeSourceId;";

                var parameters = new DynamicParameters();
                parameters.Add("@incomeSourceId", incomeSource.IncomeSourceId, DbType.Int16);
                parameters.Add("@userId", incomeSource.UserId, DbType.Int16);
                parameters.Add("@incomeSourceName", incomeSource.IncomeSourceName, DbType.String);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteIncomeSource(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE incomeSource
                                SET isDeleted = 1
                                WHERE incomeSourceId = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
