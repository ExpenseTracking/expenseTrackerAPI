using Dapper;
using expenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace expenseTrackerAPI.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly string _connectionString;

        public TransactionTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<TransactionType> GetTransactionTypes()
        {   
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT * FROM transactionTypes
                                WHERE userId IS NULL AND isDeleted = 0";
                return conn.Query<TransactionType>(sql);
            }
        }

        public IEnumerable<TransactionType> GetTransactionTypeByUserId(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT t.*,
                                       u.username 
                                FROM transactionTypes t
                                JOIN users u ON t.userId = u.userId
                                WHERE (t.userId = @id OR t.userId IS NULL) AND t.isDeleted = 0";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int16);

                return conn.Query<TransactionType>(sql, parameters);
            }
        }

        public int CreateTransactionType(TransactionType transactionType)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                // query for mysql
                //string sql = $@"INSERT INTO expenses (userId, transactionTypeId, amount, date, description, createdAt, updatedAt, deletedAt, isDeleted)
                //               VALUES (@userId, @transactionTypeId, @amount, @date, @description, @createdAt, @updatedAt, @deletedAt, @isDeleted); 
                //               SELECT LAST_INSERT_ID();";

                // query for sql server
                string sql = $@"INSERT INTO transactionTypes (userId, transactionTypeName, isDeleted)
                                 VALUES(@userId, @transactionTypeName, @isDeleted);
                                 SELECT SCOPE_IDENTITY();";

                var parameters = new DynamicParameters();
                parameters.Add("@userId", transactionType.UserId, DbType.Int16);
                parameters.Add("@transactionTypeName", transactionType.TransactionTypeName, DbType.String);
                parameters.Add("@isDeleted", 0, DbType.Boolean);

                return conn.ExecuteScalar<int>(sql, parameters);
            }
        }

        public bool UpdateTransactionType(TransactionType transactionType)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE transactionTypes 
                                SET transactionTypeName = @transactionTypeName
                                WHERE transactionTypeId = @transactionTypeId AND isDeleted = 0;";

                var parameters = new DynamicParameters();
                parameters.Add("@transactionTypeId", transactionType.TransactionTypeId, DbType.Int16);
                parameters.Add("@transactionTypeName", transactionType.TransactionTypeName, DbType.String);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }

        public bool DeleteTransactionType(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = $@"UPDATE transactionTypes
                                SET isDeleted = 1
                                WHERE transactionTypeId = @transactionTypeId";
                var parameters = new DynamicParameters();
                parameters.Add("@transactionTypeId", id, DbType.Int16);

                var rows = conn.Execute(sql, parameters);
                return rows > 0;
            }
        }
    }
}
