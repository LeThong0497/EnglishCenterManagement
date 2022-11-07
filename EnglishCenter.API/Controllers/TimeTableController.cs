using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : Controller
    {
        private readonly ITimeTableService _timeTableService;

        public TimeTableController(ITimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(List<TimeTableRequest> timeTableRequests)
        {
            await _timeTableService.AddRange(timeTableRequests);

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TimeTable>> Update([FromRoute]int id, TimeTableRequest timeTableRequest)
        {
            var result = await _timeTableService.Update(id, timeTableRequest);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeTable>> Delete([FromRoute] int id)
        {
            var result = await _timeTableService.Delete(id);

            return Ok(result);
        }

        [HttpGet("course/{id}")]
        public async Task<ActionResult<IEnumerable<TimeTableResponse>>> Gets([FromRoute]int id)
        {
            var timeTables = await _timeTableService.Gets(id);

            return Ok(timeTables);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTableResponse>>> GetAll()
        {
            var timeTables = await _timeTableService.GetAll();

            return Ok(timeTables);
        }

        [HttpGet("account/{id}")]
        public async Task<ActionResult<IEnumerable<TimeTableResponse>>> GetTimeTables([FromRoute] int id)
        {
            var timeTables = await _timeTableService.GetTimeTables(id);

            return Ok(timeTables);
        }
    }
}
