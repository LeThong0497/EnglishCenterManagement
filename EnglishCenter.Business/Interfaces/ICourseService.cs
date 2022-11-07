using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface ICourseService
    {
        Task<Course> Add(CourseRequest CourseRequest);

        Task<Course> GetById(int id);

        Task<IEnumerable<Course>> GetAll();

        Task<Course> Delete(int id);

        Task<Course> Update(int id, CourseRequest CourseRequest);
    }
}
