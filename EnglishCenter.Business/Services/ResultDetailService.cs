using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.ResultDetail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class ResultDetailService : IResultDetailService
    {
        private readonly IBaseRepository<DetailResult> _baseRepository;

        public ResultDetailService(IBaseRepository<DetailResult> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task AddRange(List<ResultDetailRequest> resultDetailRequests)
        {
            var list = new List<DetailResult>();

            for (int i = 0; i < resultDetailRequests.Count(); i++)
            {
                var detailResult = new DetailResult
                {
                    ResultId = resultDetailRequests[i].ResultId,
                    SelectedAns = resultDetailRequests[i].SelectedAns,
                    QuestionId= resultDetailRequests[i].QuestionId,
                    Ok= resultDetailRequests[i].Ok
                };

                list.Add(detailResult);
            }

            return _baseRepository.AddRange(list);
        }
    }
}
