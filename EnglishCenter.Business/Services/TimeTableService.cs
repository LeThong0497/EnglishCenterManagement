using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.TimeTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class TimeTableService : ITimeTableService
    {
        private readonly IBaseRepository<TimeTable> _baseRepository;

        public TimeTableService(IBaseRepository<TimeTable> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<TimeTable> Add(TimeTableRequest timeTableRequest)
        {
            if (timeTableRequest == null)
                return null;

            var timeTable = new TimeTable
            {
                CourseId = timeTableRequest.CourseId,
                IdZoom = timeTableRequest.IdZoom,
                PassZoom = timeTableRequest.PassZoom,
                Date = timeTableRequest.Date,
                Time = timeTableRequest.Time,
                DateStart = timeTableRequest.DateStart,
                AccountId = timeTableRequest.AccountId,
            };

            var timeTableResponse = await _baseRepository.Add(timeTable);

            return timeTableResponse;
        }

        public async Task AddRange(List<TimeTableRequest> timeTableRequests)
        {
            if (timeTableRequests == null)
                throw new Exception("Data Null");

            var timeTables = new List<TimeTable>();

            foreach(TimeTableRequest temp in timeTableRequests)
            {
                var timeTable = new TimeTable
                {
                    CourseId = temp.CourseId,
                    IdZoom = temp.IdZoom,
                    PassZoom = temp.PassZoom,
                    Date = temp.Date,
                    Time = temp.Time,
                    DateStart = temp.DateStart,
                    AccountId = temp.AccountId,
                };
                timeTables.Add(timeTable);
            }
            
           await _baseRepository.AddRange(timeTables);           
        }

        public async Task<TimeTable> Delete(int id)
        {
            var temp =await _baseRepository.GetById(id);

            if (temp == null)
                throw new Exception("Not Found");

            var result =await _baseRepository.Delete(temp);

            return result;
        }

        public async Task<IEnumerable<TimeTableResponse>> Gets(int courseId)
        {
            var timeTables = await _baseRepository.Entities
                .Include(x => x.Account)
                .ThenInclude(x => x.Course)
                .Where(x => x.CourseId.Equals(courseId))
                .Select(x => new TimeTableResponse
                {
                    Id = x.Id,
                    CourseId = x.CourseId,
                    IdZoom = x.IdZoom,
                    PassZoom = x.PassZoom,
                    Date =x.Date,
                    Time = x.Time,
                    DateStart = x.DateStart,
                    FullName =x.Account.FullName,
                    AccountId = x.AccountId,
                    CourseName =x.Course.Name,
                })
                .ToListAsync();

            if (timeTables == null)
                return null;

            return timeTables;
        }

        public async Task<IEnumerable<TimeTableResponse>> GetAll()
        {
            var timeTables = await _baseRepository.Entities
                .Include(x => x.Account)
                .ThenInclude(x => x.Course)
                .Select(x => new TimeTableResponse
                {
                    Id = x.Id,
                    CourseId = x.CourseId,
                    IdZoom = x.IdZoom,
                    PassZoom = x.PassZoom,
                    Date = x.Date,
                    Time = x.Time,
                    DateStart = x.DateStart,
                    FullName = x.Account.FullName,
                    AccountId = x.AccountId,
                    CourseName = x.Course.Name,
                })
                .ToListAsync();

            if (timeTables == null)
                return null;

            return timeTables;
        }

        public async Task<IEnumerable<TimeTableResponse>> GetTimeTables(int accId)
        {
            var timeTables = await _baseRepository.Entities
                .Include(x => x.Account)
                .ThenInclude(x => x.Course)
                .Where(x => x.AccountId.Equals(accId))
                .Select(x => new TimeTableResponse
                {
                    Id = x.Id,
                    CourseId = x.CourseId,
                    IdZoom = x.IdZoom,
                    PassZoom = x.PassZoom,
                    Date = x.Date,
                    Time = x.Time,
                    DateStart = x.DateStart,
                    FullName = x.Account.FullName,
                    AccountId =x.AccountId,
                    CourseName = x.Course.Name,
                })
                .ToListAsync();

            if (timeTables == null)
                return null;

            return timeTables;
        }

        public async Task<TimeTable> Update(int id, TimeTableRequest timeTableRequest)
        {
            var update = await _baseRepository.GetById(id);

            if (update == null)
                throw new Exception("Not found");

            update.CourseId = timeTableRequest.CourseId;
            update.IdZoom = timeTableRequest.IdZoom;
            update.PassZoom = timeTableRequest.PassZoom;
            update.Date = timeTableRequest.Date;
            update.Time = timeTableRequest.Time;
            update.DateStart = timeTableRequest.DateStart;
            update.AccountId = timeTableRequest.AccountId;
           
            var timeTableResponse = await _baseRepository.Update(update);

            return timeTableResponse;
        }
    }
}
