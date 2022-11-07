using EnglishCenter.Accessor.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface ITestDetailService
    {
        Task AddRange(List<TestDetail> testDetailRequests);
    }
}
