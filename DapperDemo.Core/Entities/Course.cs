using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperDemo.Core.Entities
{
    public class Course
    {
        private readonly IList<Student> _students= new List<Student>();

        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Student> Students => _students;

        public Course()
        {
            
        }

        public Student GetStudent(int id)
        {
            return _students.First(s => s.Id == id);
        }

        public void AddStudent(Student newStudent)
        {
            _students.Add(newStudent);
        }

        public bool RemoveStudent(Student student)
        {
            return _students.Remove(student);
        }
    }
}
