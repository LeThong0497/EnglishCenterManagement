using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Question;
using EnglishCenter.Common.Models.Result;
using EnglishCenter.Common.Models.ResultDetail;
using EnglishCenter.Common.Models.Test;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class TestService : ITestService
    {
        private readonly IBaseRepository<Test> _baseRepository;
        private readonly IQuestionService _questionService;
        private readonly ITestDetailService _testDetailService;
        private readonly IAccountService _accountService;
        private readonly IBaseRepository<Result> _baseRepositoryR;
        private readonly IResultDetailService _resultDetailService;
        private readonly IResultService _resultService;


        public TestService(IBaseRepository<Test> baseRepository, IQuestionService questionService,
                             ITestDetailService testDetailService, IAccountService accountService,
                             IBaseRepository<Result> baseRepositoryR, IResultDetailService resultDetailService,
                              IResultService resultService)
        {
            _baseRepository = baseRepository;
            _questionService = questionService;
            _testDetailService = testDetailService;
            _accountService = accountService;
            _baseRepositoryR = baseRepositoryR;
            _resultDetailService = resultDetailService;
            _resultService = resultService;
        }

        //Add test
        public async Task<Test> Add(TestRequest testRequest)
        {
            var test = new Test
            {
                TestName = testRequest.TestName,
                DateCreated = DateTime.Now,
                State = true,
                CourseId = testRequest.CourseId,
                Createsby = testRequest.Createsby,
                DateTest = testRequest.DateTest,
                Time = testRequest.Time,
                TotalQuestion = testRequest.TotalQuestion
            };

            var newTest = await _baseRepository.Add(test);

            var list = new List<TestDetail>();

            var listIdQuestion = (await _questionService.GetAll()).Select(x => x.QuestionId).ToList();
            var listTestId = new List<int>();
            Random r = new Random();
            for (int i = 0; i < testRequest.TotalQuestion; i++)
            {
                int idTemp = listIdQuestion[r.Next(0, listIdQuestion.Count() - 1)];
                listTestId.Add(idTemp);
                listIdQuestion.Remove(idTemp);

            }
            for (int i = 0; i < listTestId.Count(); i++)
            {
                var testDetail = new TestDetail
                {
                    TestId = newTest.TestId,
                    QuestionId = listTestId[i]

                };
                list.Add(testDetail);
            }

            await _testDetailService.AddRange(list);

            return newTest;
        }

        //Get test by id
        public async Task<TestResponse> GetTestDetail(int id)
        {
            var test = await _baseRepository.Entities
                .Include("TestDetails")
                .Where(x => x.TestId == id)
                .FirstAsync();

            if (test == null)
            {
                throw new Exception("Not Found!");
            }

            var tests = new List<Test>();
            tests.Add(test);
            var testDetail = (await GetListTestDetail(tests))[0];

            return testDetail;
        }

        //get all test
        public async Task<IEnumerable<TestResponse>> GetTests()
        {
            var lists = await _baseRepository.Entities
                .Include("TestDetails")
                .ToListAsync();

            if (lists == null)
            {
                return null;
            }

            var listDetailTests = await GetListTestDetail(lists);

            return listDetailTests;
        }

        //get course by courser
        public async Task<IEnumerable<TestResponse>> GetTestsByCourse(int courseId)
        {
            var lists = await _baseRepository.Entities
                .Include("TestDetails")
                .Where(x => x.CourseId == courseId)
                .ToListAsync();

            if (lists == null)
            {
                return null;
            }

            var listDetailTests = await GetListTestDetail(lists);

            return listDetailTests;
        }

        //get test new
        public async Task<IEnumerable<TestResponse>> GetTestsUpComming()
        {
            var lists = await _baseRepository.Entities
                .Include("TestDetails")
                .Where(x => x.State == true)
                .ToListAsync();

            if (lists == null)
            {
                return null;
            }

            var listDetailTests = await GetListTestDetail(lists);

            return listDetailTests;
        }

        //get test new by course
        public async Task<IEnumerable<TestResponse>> GetTestByCousrse(int courseId)
        {
            var lists = await _baseRepository.Entities
                .Include("TestDetails")
                .Where(x => x.CourseId.Equals(courseId) && x.State == true)
                .ToListAsync();

            if (lists == null)
            {
                return null;
            }

            var listDetailTests = await GetListTestDetail(lists);

            return listDetailTests;
        }

        //get test old       
        public async Task<IEnumerable<Test>> GetTestsFinished()
        {
            var listTests = await _baseRepository.Entities.Where(x => x.State == false).ToListAsync();

            if (listTests == null)
            {
                return null;
            }
            return listTests;
        }

        //Test old by course        
        public async Task<IEnumerable<Test>> GetTestByCousrseFinished(int courseId)
        {
            var listTests = await _baseRepository.Entities
                .Where(x => x.CourseId.Equals(courseId) && x.State == false)
                .ToListAsync();

            if (listTests == null)
            {
                return null;
            }
            return listTests;
        }

        //test overdue for one account          
        public async Task<IEnumerable<Test>> GetTestCompleted(int accountid)
        {
            var account = await _accountService.GetById(accountid);

            if (account == null)
            {
                return null;
            }

            var listResultsId = await _baseRepositoryR.Entities.Where(x => x.AccountId == accountid).Select(x => x.TestId).ToListAsync();
            var listCompletedTestId = await _baseRepository.Entities
                .Where(x => x.State == false && x.CourseId == account.CourseId)
                .Select(x => x.TestId)
                .ToListAsync();

            var listOverdueTestsId = new List<int>();
            for (int i = 0; i < listCompletedTestId.Count(); i++)
            {
                if (listResultsId.IndexOf(listCompletedTestId[i]) == -1)
                {
                    listOverdueTestsId.Add(listCompletedTestId[i]);
                }
            }

            var tests = new List<Test>();
            for (int i = 0; i < listOverdueTestsId.Count(); i++)
            {
                var tem = await _baseRepository.Entities.Where(x => x.TestId == listOverdueTestsId[i]).FirstOrDefaultAsync();
                tests.Add(tem);
            }

            return tests;
        }

        public async Task<Test> DeleteTest(int id)
        {
            var test = await _baseRepository.GetById(id);

            if (test == null)
            {
                throw new Exception("Not Found");
            }

            return await _baseRepository.Delete(test);

        }

        public async Task<Test> UpdateTest(int id, TestEditRequest testRequest)
        {
            var test = await _baseRepository.GetById(id);

            if (test == null)
            {
                throw new Exception("Not Found");
            }

            test.CourseId = testRequest.CourseId;
            test.TestName = testRequest.TestName;
            test.Createsby = testRequest.Createsby;
            test.DateCreated = testRequest.DateCreated;
            test.DateTest = testRequest.DateTest;
            test.Time = testRequest.Time;
            test.State = testRequest.State;

            return await _baseRepository.Update(test);
        }

        //submit the test        
        public async Task SubmitTest(ResultRequest resultRequest)
        {
            var test = await _baseRepository.GetById(resultRequest.TestId);

            if (test == null)
            {
                throw new Exception("Can not find this test!");
            }


            var result = new Result
            {
                AccountId = resultRequest.AccountId,
                TestId = resultRequest.TestId,
                Score = resultRequest.Score,
                Date = DateTime.Now
            };

            var resultNew = await _resultService.Add(result);

            var list = new List<ResultDetailRequest>();
            for (int i = 0; i < resultRequest.Answers.Count(); i++)
            {
                var detailResult = new ResultDetailRequest
                {
                    ResultId = resultNew.ResultId,
                    SelectedAns = resultRequest.Answers[i].Answer,
                    QuestionId = resultRequest.Answers[i].QuestionId,
                    Ok = resultRequest.Answers[i].Ok
                };
                list.Add(detailResult);
            }

            await _resultDetailService.AddRange(list);

        }

        public async Task<bool> IsDoing(int testId, int accountId)
        {
            var result = await _baseRepositoryR.Entities
                        .Where(x => x.AccountId.Equals(accountId) && x.TestId.Equals(testId))
                        .FirstOrDefaultAsync();

            if (result == null)
                return false;

            return true;
        }

        private async Task<List<TestResponse>> GetListTestDetail(List<Test> lists)
        {
            var listDetailTests = new List<TestResponse>();

            for (int i = 0; i < lists.Count(); i++)
            {
                var testDetail = new TestResponse();

                testDetail.TestId = lists[i].TestId;
                testDetail.TestName = lists[i].TestName;
                testDetail.CourseId = lists[i].CourseId;
                testDetail.DateCreated = lists[i].DateCreated;
                testDetail.Time = lists[i].Time;
                testDetail.State = lists[i].State;
                testDetail.Createsby = lists[i].Createsby;
                testDetail.DateTest = lists[i].DateTest;
                testDetail.TotalQuestion = lists[i].TotalQuestion;

                var listQuestions = new List<QuestionResponse>();
                for (int j = 0; j < lists[i].TestDetails.Count(); j++)
                {
                    var temp = await _questionService.GetById(lists[i].TestDetails[j].QuestionId);
                    var tem = new QuestionResponse
                    {
                        QuestionId = temp.QuestionId,
                        Content = temp.Content,
                        AnswerA = temp.AnswerA,
                        AnswerB = temp.AnswerB,
                        AnswerC = temp.AnswerC,
                        AnswerD = temp.AnswerD,
                        CorectAns = temp.CorectAns
                    };

                    listQuestions.Add(tem);
                }
                testDetail.ListQuestions = listQuestions;
                listDetailTests.Add(testDetail);
            }

            return listDetailTests;
        }

        public async Task<bool> ChangeState()
        {
            var tests = await _baseRepository.Entities
                                    .Where(x =>x.DateTest.AddMinutes(10) < DateTime.Now && x.State==true)
                                    .ToListAsync();

            var x = (await _baseRepository.GetById(52)).DateTest.AddMinutes(10);
            var y = DateTime.Now;
           var r = x < y;
            if (tests == null)
                return false;

            foreach (Test t in tests)
            {
                t.State = false;
                await _baseRepository.Update(t);
            }

            return true;
        }

        public async Task<Test> CloseTest(int id)
        {
            var test = await _baseRepository.GetById(id);
                                    
            if (test == null)
                throw new Exception("NotFound") ;

                test.State = false;
                await _baseRepository.Update(test);
            
            return test;
        }
    }
}
