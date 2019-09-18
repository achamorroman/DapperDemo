using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace DapperDemo.Models
{
    // Los data annotations se incluyen con el paquete Dapper.Contrib

    [Table("Courses")] 
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationHours { get; set; }
        public double Price { get; set; }

        // Indicamos a Dapper que esta propiedad no es de escritura. 
        // De no hacerlo fallaría el método extensor .Insert()
        [Write(false)]
        public IList<Student> Students { get; set; } 
    }
}
