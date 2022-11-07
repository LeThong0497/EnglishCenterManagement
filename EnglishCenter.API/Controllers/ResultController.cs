using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Result;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDetailResponse>>> GetResultsByAccount(int accId)
        {
            var list = await _resultService.GetResultByAccount(accId);

            return Ok(list);
        }

        [HttpGet("test/{id}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByTest([FromRoute]int id)
        {
            var list = await _resultService.GetResultByTest(id);

            return Ok(list);
        }
    }
}
