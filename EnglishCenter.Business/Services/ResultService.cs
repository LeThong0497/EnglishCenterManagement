using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Result;
using EnglishCenter.Common.Models.ResultDetail;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class ResultService : IResultService
    {
        private readonly IBaseRepository<Result> _baseRepository;

        public ResultService(IBaseRepository<Result> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<Result> Add(Result result)
        {          
            return  await  _baseRepository.Add(result);
        }

        public async Task<IEnumerable<ResultDetailResponse>> GetResultByAccount(int accId)
        {
            var results =await _baseRepository.Entities
                                      .Include(x => x.DetailResults)
                                      .Where(x => x.AccountId.Equals(accId))
                                      .ToListAsync();

            if (results == null)
                return null;

           var resultDetails= results.Select(x => new ResultDetailResponse
            {
                TestId = x.TestId,
                Score = x.Score,
                Date = x.Date,
                DetailResults = x.DetailResults.Select(x => new ResultDetailRequest
                {
                    ResultId=x.ResultId,
                    SelectedAns=x.SelectedAns,
                    QuestionId=x.QuestionId,
                    Ok=x.Ok
                }).ToList(),
            });

            return resultDetails;
        }

        public async Task<IEnumerable<ResultResponse>> GetResultByTest(int id)
        {
            var results = await _baseRepository.Entities
                                      .Include(x => x.Account)
                                      .Include(x => x.Test)
                                      .Where(x => x.TestId.Equals(id))
                                      .Select(x => new ResultResponse
                                      {
                                          FullName = x.Account.FullName,
                                          TestId=x.TestId,
                                          TestName=x.Test.TestName,
                                          Score=x.Score,
                                          Date=x.Date
                                      })
                                      .ToListAsync();

            if (results == null)
                return null;
            
            return results;
        }
    }
}
