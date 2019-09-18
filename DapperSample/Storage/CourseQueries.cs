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

        public IEnumerable<Course> GetWithChilds()
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"SELECT 
	                            C.ID,
	                            C.Name, 
	                            C.Description, 
	                            C.DurationHours,
	                            C.Price,
	                            S.ID,
	                            S.FirstName,
	                            S.LastName,
	                            S.Email
                            FROM 
	                            courses C 
                            LEFT JOIN 
	                            CoursesStudents CS on c.ID = cs.CourseId
                            LEFT JOIN 
	                            Students S on CS.StudentId = S.ID";

                try
                {
                    var courseDictionary = new Dictionary<int, Course>();

                    dbConnection.Open();
                    return dbConnection.Query<Course,Student,Course>(sql,
                        (course, student) =>
                        {
                            Course courseEntry;
                            if (!courseDictionary.TryGetValue(course.Id, out courseEntry))
                            {
                                courseEntry = course;
                                courseEntry.Students = new List<Student>();
                                courseDictionary.Add(courseEntry.Id, courseEntry);
                            }

                            courseEntry.Students.Add(student);
                            return courseEntry;
                        })
                        .Distinct()
                        .ToList();
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
