using Manage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Services
{
    internal class ExpenseService
    {
        private const string TABLE_NAME = "Expense";

        public static async Task<List<Expense>> GetAllExpenseAsync()
        {
            string query = $"SELECT * FROM {TABLE_NAME};";
            return await DataAccessLayer.ExecuteQueryAsync(query, ReaderToExpenseList);
        }

        public static async Task<Expense> GetExpenseById(int id)
        {
            string query = $"SELECT * FROM {TABLE_NAME} WHERE Id = {id}";
            return await DataAccessLayer.ExecuteQueryAsync(query, ReadToExpense);
        }

        public static async Task CreateExpense(Expense newExpense)
        {
            ThrowIfNull(newExpense);

            string command = $"INSERT INTO {TABLE_NAME} (Category_Id,Description,Amount  ) " +
                 $"VALUES ({newExpense.CategoryId},'{newExpense.Description}', {newExpense.Amount} )";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        public static async Task UpdateExpense(Expense ExpenseToUpdate)
        {
            ThrowIfNull(ExpenseToUpdate);

            string command = $"UPDATE {TABLE_NAME} " +
                             $"SET Category_Id = {ExpenseToUpdate.CategoryId}, " +
                             $"Description = '{ExpenseToUpdate.Description}', " +
                             $"Amount = {ExpenseToUpdate.Amount}, " +
                             $"Date = '{ExpenseToUpdate.Date:yyyy-MM-dd HH:mm:ss}' " +
                             $"WHERE Id = {ExpenseToUpdate.Id}";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }


        public static async Task DeleteExpense(int id)
        {
            string command = $"DELETE FROM {TABLE_NAME} WHERE Id = {id}";
            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        private static Expense ReadToExpense(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            if (reader.HasRows)
            {
                Expense expense = null;
                while (reader.Read())
                {
                    expense = new Expense
                    {
                        Id = reader.GetInt32(0),
                        CategoryId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        Amount = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4),

                    };
                }

                return expense;
            }

            return null;
        }

        private static List<Expense> ReaderToExpenseList(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            List<Expense> result = new List<Expense>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Expense expense = new Expense
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        CategoryId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Amount = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        Date = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    };

                    result.Add(expense);
                }
            }

            return result;
        }

        private static void ThrowIfNull<T>(T value) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
