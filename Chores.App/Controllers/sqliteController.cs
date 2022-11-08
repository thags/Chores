using System;
using System.Xml.Linq;
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
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS chores(
                        id INTEGER PRIMARY KEY, 
                        Name TEXT, 
                        Notes TEXT, 
                        CompletionDate DATETIME,
                        NextDueDate DATETIME,
                        Recurrence TIMESPAN
                        );";
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
            List<Chore> chores = new List<Chore>();
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
                            chores.Add(
                                new Chore
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Note = reader.GetString(2),
                                    CompletionDate = reader.GetDateTime(3),
                                    NextDueDate = reader.GetDateTime(4),
                                    Recurrence = reader.GetTimeSpan(5)
                                });
                        }
                    }
                }
            }
            return chores;
        }

        public void AddChore(Chore newChore)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"INSERT INTO chores(Name, Notes, CompletionDate, NextDueDate, Recurrence) 
                        VALUES(
                        '{newChore.Name}', 
                        '{newChore.Note}', 
                        '{newChore.CompletionDate}', 
                        '{newChore.NextDueDate}', 
                        '{newChore.Recurrence}'
                        )";

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
                    command.CommandText = $@"UPDATE chores SET 
                        Name = '{editedChore.Name}', 
                        Notes = '{editedChore.Note}', 
                        CompletionDate='{editedChore.CompletionDate}' 
                        NextDueDate = '{editedChore.NextDueDate}' 
                        Recurrence = '{editedChore.Recurrence}'
                        WHERE Id = '{editedChore.Id}'";

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

        public Chore GetChore(int id)
        {
            Chore chore = new Chore();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM chores where Id = {id}";


                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chore.Id = reader.GetInt32(0);
                            chore.Name = reader.GetString(1);
                            chore.Note = reader.GetString(2);
                            chore.CompletionDate = reader.GetDateTime(3);
                            chore.NextDueDate = reader.GetDateTime(4);
                            chore.Recurrence = reader.GetTimeSpan(5);
                        }
                    }
                }
            }
            return chore;
        }
    }
}

