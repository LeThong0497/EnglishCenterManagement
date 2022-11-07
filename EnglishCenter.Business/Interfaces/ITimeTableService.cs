using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.TimeTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
   public interface ITimeTableService
    {
        Task<TimeTable> Add(TimeTableRequest timeTableRequest);

        Task AddRange(List<TimeTableRequest> timeTableRequests);

        Task<IEnumerable<TimeTableResponse>> Gets(int courseId);

        Task<IEnumerable<TimeTableResponse>> GetAll();

        Task<IEnumerable<TimeTableResponse>> GetTimeTables(int accId);

        Task<TimeTable> Update(int id, TimeTableRequest timeTableRequest);

        Task<TimeTable> Delete(int id);
    }
}
