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

        public IEnumerable<CourseResume> GetStudentsByCourse()
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"select C.ID, C.Description, C.Name, Count(S.ID) as StudentCount 
from courses C inner join CoursesStudents CS on c.ID = cs.CourseId inner join Students S on S.ID = cs.StudentId
group by C.ID, C.Description, C.Name";

                try
                {
                    dbConnection.Open();
                    return dbConnection.Query<CourseResume>(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }

    public class CourseResume 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StudentCount { get; set; }
    }
}
