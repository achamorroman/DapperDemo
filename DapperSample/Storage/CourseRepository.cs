using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperSample.Models;

namespace DapperSample.Storage
{
    public class CourseRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public CourseRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void Add(Course newCourse)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"INSERT INTO Courses (Name, Description, DurationHours, Price)
                            VALUES(@Name, @Description, @DurationHours, @Price)";
                try
                {
                    dbConnection.Open();
                    dbConnection.Execute(sql, newCourse);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        public void Update(Course course)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"UPDATE Courses SET 
                                Name = @Name,
                                Description = @Description, 
                                DurationHours = @DurationHours,
                                Price = @Price
                            WHERE 
                                ID = @Id";

                try
                {
                    dbConnection.Open();
                    dbConnection.Query(sql, course);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = "DELETE FROM Courses WHERE ID = @Id";

                try
                {
                    dbConnection.Open();
                    dbConnection.Execute(sql, new { Id = id });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
