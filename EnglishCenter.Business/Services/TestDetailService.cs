using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class TestDetailService : ITestDetailService
    {
        private readonly IBaseRepository<TestDetail> _baseRepository;

        public TestDetailService(IBaseRepository<TestDetail> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public  Task AddRange(List<TestDetail> testDetailRequests)
        {
            if(testDetailRequests==null)
            {
              return  null;
            }

            return  _baseRepository.AddRange(testDetailRequests);
        }
    }
}
