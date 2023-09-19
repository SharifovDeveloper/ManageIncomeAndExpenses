using Manage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Manage.Services
{
    internal class IncomeService
    {
        private const string TABLE_NAME = "Income";

        public static async Task<List<Income>> GetAllIncomeAsync()
        {
            string query = $"SELECT * FROM {TABLE_NAME};";
            return await DataAccessLayer.ExecuteQueryAsync(query, ReaderToIncomeList);
        }

        public static async Task<Income> GetIncomeById(int id)
        {
            string query = $"SELECT * FROM {TABLE_NAME} WHERE Id = {id}";
            return await DataAccessLayer.ExecuteQueryAsync(query, ReadToIncome);
        }

        public static async Task CreateIncome(Income newIncome)
        {
            ThrowIfNull(newIncome);

            string command = $"INSERT INTO {TABLE_NAME} (Category_Id,Description,Amount  ) " +
                 $"VALUES ({newIncome.CategoryId},'{newIncome.Description}', {newIncome.Amount} )";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        public static async Task UpdateIncome(Income IncomeToUpdate)
        {
            ThrowIfNull(IncomeToUpdate);

            string command = $"UPDATE {TABLE_NAME} " +
                             $"SET Category_Id = {IncomeToUpdate.CategoryId}, " +
                             $"Description = '{IncomeToUpdate.Description}', " +
                             $"Amount = {IncomeToUpdate.Amount}, " +
                             $"Date = '{IncomeToUpdate.Date:yyyy-MM-dd HH:mm:ss}' " +
                             $"WHERE Id = {IncomeToUpdate.Id}";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }


        public static async Task DeleteIncome(int id)
        {
            string command = $"DELETE FROM {TABLE_NAME} WHERE Id = {id}";
            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        private static Income ReadToIncome(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            if (reader.HasRows)
            {
                Income income = null;
                while (reader.Read())
                {
                    income = new Income
                    {
                        Id = reader.GetInt32(0),
                        CategoryId = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        Amount = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4),

                    };
                }

                return income;
            }

            return null;
        }

        private static List<Income> ReaderToIncomeList(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            List<Income> result = new List<Income>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Income income = new Income
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        CategoryId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Amount = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        Date = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    };

                    result.Add(income);
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
