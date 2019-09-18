using System.Collections.Generic;
using DapperDemo.Models;
using DapperDemo.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseQueries _courseQueries;

        public CoursesController()
        {
            _courseRepository = new CourseRepository(new SqlConnectionProvider());
            _courseQueries=new CourseQueries(new SqlConnectionProvider());
        }

        // GET: api/courses
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseQueries.GetAll();
        }

        [Route("StudentCount")]
        [HttpGet]
        public IEnumerable<CourseResume> GetStudentCount()
        {
            return _courseQueries.GetStudentsByCourse();
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return _courseQueries.GetById(id);
        }

        // POST api/couses
        [HttpPost]
        public void Post([FromBody]Course newCourse)
        {
            if (ModelState.IsValid)
                _courseRepository.Add(newCourse);
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Course course)
        {
            course.Id = id;
            if (ModelState.IsValid)
                _courseRepository.Update(course);
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }
    }
}