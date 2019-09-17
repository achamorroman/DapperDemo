using System.Collections.Generic;

namespace DapperDemo.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationHours { get; set; }
        public double Price { get; set; }

        public IList<Student> Students { get; set; } 
    }
}
