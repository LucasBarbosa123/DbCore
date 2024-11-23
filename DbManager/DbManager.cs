using DbManagerService.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagerService
{
    public class DbManager : IDbManager
    {
        private string CONN_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=DbCore;Trusted_Connection=True;MultipleActiveResultSets=true";
        public void CreateDatabase(string name)
        {
            try
            {
                var query = $"CREATE DATABASE {name}";
                using (SqlConnection connection = new SqlConnection(CONN_STRING))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTable(string dbName, string tableName, List<Column> columns)
        {
            try
            {
                var columnsPart = ColumnPartConstructor(columns);

                var query = $@"USE {dbName};
                           CREATE TABLE {tableName}(
                               {columnsPart}
                           );";
                using (SqlConnection connection = new SqlConnection(CONN_STRING))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ColumnPartConstructor(List<Column> columns)
        {
            try
            {
                var columnsPart = "";
                for (int i = 0; i < columns.Count; i++)
                {
                    var constraigts = string.Join(" ", columns[i].Constraigts);
                    var colLine = "";

                    //if it isn't the last column we put the , at the end
                    if (i != columns.Count - 1)
                    {
                        colLine = $@"{columns[i].Name} {columns[i].Type} {constraigts},
                        ";
                    }
                    else
                    {
                        colLine = $@"{columns[i].Name} {columns[i].Type} {constraigts}
                        ";
                    }

                    columnsPart = $"{columnsPart}{colLine}";
                }

                return columnsPart;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
