using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly IBaseRepository<Course> _baseRepository;

        public CourseService(IBaseRepository<Course> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<Course> Add(CourseRequest courseRequest)
        {
            var course = new Course
            {
                Name = courseRequest.Name,
                Description = courseRequest.Description,
                TimeStudy = courseRequest.TimeStudy
            };

            return await _baseRepository.Add(course);
        }

        public async Task<Course> Delete(int id)
        {
            var course = await _baseRepository.GetById(id);

            if (course==null)
            {
                throw new Exception("Not Found");
            }

            return await _baseRepository.Delete(course);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Course> GetById(int id)
        {
            var course= await _baseRepository.GetById(id);

            if (course == null)
            {
                throw new Exception("Not Found");
            }

            return course;
        }

        public async Task<Course> Update(int id, CourseRequest CourseRequest)
        {
            var course = await _baseRepository.GetById(id);

            if (course == null)
            {
                throw new Exception("Not Found");
            }

            course.Name = CourseRequest.Name;
            course.Description = CourseRequest.Description;
            course.TimeStudy = CourseRequest.TimeStudy;

            return await _baseRepository.Update(course);
        }
    }
}
