using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public IEnumerable<CourseSummary> GetCourseSummary()
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"SELECT 
	                            C.ID, 
	                            C.Description, 
	                            C.Name, 
	                            Count(CS.StudentId) as StudentCount 
                            FROM 
	                            courses C 
                            INNER JOIN 
	                            CoursesStudents CS on c.ID = cs.CourseId
                            GROUP BY 
	                            C.ID, C.Description, C.Name";

                try
                {
                    dbConnection.Open();
                    return dbConnection.Query<CourseSummary>(sql);
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
