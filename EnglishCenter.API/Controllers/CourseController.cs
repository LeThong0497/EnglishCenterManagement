using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Course;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglisCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _courseService.GetAll();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseRequest courseRequest)
        {
            var course = await _courseService.Add(courseRequest);
            return Ok(course);
        }

        [HttpPut]
        public async Task<ActionResult<Course>> UpdateCourse(int id, CourseRequest courseRequest)
        {
            var course = await _courseService.Update(id, courseRequest);
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _courseService.Delete(id);
            return Ok(course);
        }
    }
}
