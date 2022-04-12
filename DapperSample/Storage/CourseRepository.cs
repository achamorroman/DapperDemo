using System;
using Dapper;
using Dapper.Contrib.Extensions;
using DapperDemo.Models;

namespace DapperDemo.Storage
{
    public class CourseRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public CourseRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public Course Add(Course newCourse)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = @"INSERT INTO Courses (Name, Description, DurationHours, Price)
                            VALUES(@Name, @Description, @DurationHours, @Price)";

                dbConnection.Execute(sql, newCourse);
                return newCourse;
            }
        }

        public Course AddContrib(Course newCourse)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                dbConnection.Insert<Course>(newCourse);
                return newCourse;
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

                dbConnection.Query(sql, course);
            }
        }

        public void UpdateContrib(Course course)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                dbConnection.Update<Course>(course);
            }
        }

        public void Delete(int id)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var sql = "DELETE FROM Courses WHERE ID = @Id";
                dbConnection.Execute(sql, new { Id = id });
            }
        }

        public void DeleteContrib(int id)
        {
            using (var dbConnection = _connectionProvider.GetNewConnection)
            {
                var course = dbConnection.Get<Course>(id);
                dbConnection.Delete(course);
            }
        }

    }


}
