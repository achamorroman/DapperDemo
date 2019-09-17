using Dapper.Contrib.Extensions;

namespace DapperSample.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Computed]
        public string FullName => $@"{FirstName} {LastName}";
    }
}