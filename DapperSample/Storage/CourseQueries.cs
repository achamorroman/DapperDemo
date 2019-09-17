using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DapperDemo.Models;

namespace DapperDemo.Storage
{
    public class CourseQueries
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public CourseQueries(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IEnumerable<Course> GetAll()
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"SELECT 
	                            ID, 
	                            Name, 
	                            Description, 
	                            DurationHours, 
	                            Price
                            FROM   
	                            Courses";

                try
                {
                    dbConnection.Open();
                    return dbConnection.Query<Course>(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public Course GetById(int id)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"SELECT 
                                ID, 
                                Name, 
                                Description, 
                                DurationHours, 
                                Price 
                            FROM 
                                Courses 
                            WHERE 
                                Id = @Id";
                try
                {
                    dbConnection.Open();
                    return dbConnection.Query<Course>(sql, new { Id = id }).FirstOrDefault();
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
