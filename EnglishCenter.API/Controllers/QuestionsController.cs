using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Question;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglisCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
          var list= await  _questionService.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(QuestionRequest questionRequest)
        {
            var question =await _questionService.Add(questionRequest);
            return Ok(question);
        }

        //post list question
        [HttpPost("questions")]
        public async Task<IActionResult> PostListQuestions(List<QuestionRequest> questionRequests)
        {
            await _questionService.AddRange(questionRequests);
            return StatusCode(200);           
        }

        [HttpPut]
        public async Task<ActionResult<Question>> UpdateQuestion(int id,QuestionRequest questionRequest)
        {
            var question = await _questionService.Update(id, questionRequest);
            return Ok(question);
        }

        [HttpDelete]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question =await _questionService.Delete(id);
            return Ok(question);
        }
    }
}
