using EnglishCenter.Common.Models.ResultDetail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface IResultDetailService
    {
        Task AddRange(List<ResultDetailRequest> resultDetailRequests);
    }
}
