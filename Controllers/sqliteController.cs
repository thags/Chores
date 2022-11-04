using System;
using Chores.Interfaces;
using Chores.Models;
using Microsoft.Data.Sqlite;

namespace Chores.Controllers
{
    public class sqliteController : IDBInterface
    {
        private string ConnectionString;

        public sqliteController(string connectionString = "Data Source=data.db")
        {
            ConnectionString = connectionString;
        }

        public void CreateTables()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS chores(id INTEGER PRIMARY KEY, Name TEXT, Notes TEXT, CompletionDate DATETIME);";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public List<Chore> GetAllChores()
        {
            List<Chore> drives = new List<Chore>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM 'chores' ORDER BY ID DESC";


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            drives.Add(
                                new Chore
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Notes = reader.GetString(2),
                                    CompletionDate = reader.GetDateTime(3)
                                });
                        }
                    }
                }
            }
            return drives;
        }

        public void AddChore(Chore newChore)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO chores(Name, Notes, CompletionDate) VALUES('{newChore.Name}','{newChore.Notes}', '{newChore.CompletionDate}')";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public void EditChore(Chore editedChore)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"UPDATE chores SET Name = '{editedChore.Name}', Notes = '{editedChore.Notes}', CompletionDate='{editedChore.CompletionDate}' WHERE Id = '{editedChore.Id}'";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public void DeleteChore(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM chores WHERE Id = {id}";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqliteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}

