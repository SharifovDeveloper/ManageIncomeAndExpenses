﻿using Manage.Helpers;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Manage.Services
{
    internal static class DataAccessLayer
    {
        public const string Connection_String = "Data Source=DESKTOP-B7KIDHK\\SQLEXPRESS02;Initial Catalog=Manage;Integrated Security=True";

        public static async Task ExecuteNonQueryAsync(string command)
        {
            ThrowIfNullOrEmpty(command);

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection_String))
                {
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        int affectedRows = await sqlCommand.ExecuteNonQueryAsync();
                        ConsoleHelper.WriteSuccess($"Number of affected rows: {affectedRows}");
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleDatabaseError(ex);
            }
            catch (Exception ex)
            {
                HandleGeneralError(ex);
            }
        }

        public static async Task<T> ExecuteQueryAsync<T>(string command, Func<SqlDataReader, T> converter)
        {
            ThrowIfNullOrEmpty(command);

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection_String))
                {
                    connection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        var dataReader = await sqlCommand.ExecuteReaderAsync();
                        return converter(dataReader);
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleDatabaseError(ex);
            }
            catch (Exception ex)
            {
                HandleGeneralError(ex);
            }

            return default;
        }

        private static void ThrowIfNullOrEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(nameof(str));
            }
        }

        private static void HandleDatabaseError(SqlException ex)
        {
            ConsoleHelper.WriteLineError($"Database error: {ex.Message}.");
        }

        private static void HandleGeneralError(Exception ex)
        {
            ConsoleHelper.WriteLineError($"Something went wrong: {ex.Message}.");
        }
    }
}
