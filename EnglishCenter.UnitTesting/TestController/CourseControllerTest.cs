using EnglisCenter.Accessor;
using EnglisCenter.API.Controllers;
using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Services;
using EnglishCenter.Common.Models.Course;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace EnglishCenter.UnitTesting.TestController
{
    public class CourseControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly EnglishForStudentDB _dbContext;
        private readonly BaseRepository<Course> _courseRepository;
        public CourseControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _dbContext = _fixture.Context;
            _courseRepository = new BaseRepository<Course>(_dbContext);
        }

        [Fact]
        public async Task PostCourse_Success()
        {
            var course = new CourseRequest 
            {
                Name = "Toeic 500",
                Description = "Khóa học ôn thi dành cho người mất gốc",
                TimeStudy = "3 month"
            };

            var courseService = new CourseService(_courseRepository);

            var controller = new CourseController(courseService);

            //action
            var result = await controller.PostCourse(course);

            //course
            
            result.Should().NotBeNull();

            var createdAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Course>(createdAtActionResult.Value);

            Assert.Equal("Toeic 500", returnValue.Name);
            Assert.Equal("Khóa học ôn thi dành cho người mất gốc", returnValue.Description);
            Assert.Equal("3 month", returnValue.TimeStudy);
        }

        [Fact]
        public async Task Update_Success()
        {
            var course = new Course
            {
                CourseId =1,
                Name = "Toeic 500",
                Description = "Khóa học ôn thi dành cho người mất gốc",
                TimeStudy = "3 month"
            };

            await _courseRepository.Add(course);

            var courseUpdate = new CourseRequest
            {
                Name = "Toeic 700",
                Description = "Khóa học ôn thi dành cho người mất gốc",
                TimeStudy = "3 month"
            };

            var courseService = new CourseService(_courseRepository);

            var controller = new CourseController(courseService);

            //action
            var result = await controller.UpdateCourse(1, courseUpdate);

            //course

            result.Should().NotBeNull();

            var response = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Course>(response.Value);

            Assert.Equal("Toeic 700", returnValue.Name);           
        }

        [Fact]
        public async Task GetAll_Success()
        {
            var course1 = new Course
            {
                CourseId = 1,
                Name = "Toeic 500",
                Description = "Khóa học ôn thi dành cho người mất gốc",
                TimeStudy = "3 month"
            };

            var course2 = new Course
            {
                CourseId = 2,
                Name = "Ielt 6.",
                Description = "Khóa học luyện thi ielt",
                TimeStudy = "7 month"
            };

            await _courseRepository.Add(course1);
            await _courseRepository.Add(course2);

            var courseService = new CourseService(_courseRepository);

            var controller = new CourseController(courseService);

            //action
            var result = await controller.GetCourses();

            //course

            result.Should().NotBeNull();

            var response = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Course>>(response.Value);

            returnValue.Should().HaveCount(2);
        }
    }
}
