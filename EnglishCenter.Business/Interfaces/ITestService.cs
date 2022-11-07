using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.Result;
using EnglishCenter.Common.Models.Test;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface ITestService
    {
        Task<Test> Add(TestRequest testRequest);

        Task<TestResponse> GetTestDetail(int id);

        Task<IEnumerable<TestResponse>> GetTests();

        Task<IEnumerable<TestResponse>> GetTestsByCourse(int courseId);

        Task<IEnumerable<TestResponse>> GetTestsUpComming();

        Task<IEnumerable<TestResponse>> GetTestByCousrse(int courseId);

        Task<IEnumerable<Test>> GetTestsFinished();

        Task<IEnumerable<Test>> GetTestByCousrseFinished(int courseId);

        Task<IEnumerable<Test>> GetTestCompleted(int accountid);

        Task<Test> DeleteTest(int id);

        Task<Test> UpdateTest(int id, TestEditRequest testRequest);

        Task SubmitTest(ResultRequest resultRequest);

        Task<bool> IsDoing(int testId, int accountId);

        Task<bool> ChangeState();

        Task<Test> CloseTest(int id);
    }
}
