using EnglisCenter.Accessor;
using EnglisCenter.API.Controllers;
using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Services;
using EnglishCenter.Common.Models.Question;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EnglishCenter.UnitTesting.TestController
{
    public class QuestionControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly EnglishForStudentDB _dbContext;
        private readonly BaseRepository<Question> _questionRepository;
        public QuestionControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _dbContext = _fixture.Context;
            _questionRepository = new BaseRepository<Question>(_dbContext);
        }

        [Fact]
        public async Task PostQuestion_Success()
        {
            var question = new QuestionRequest
            {
                Content = "This is ... car?",
                AnswerA = "a",
                AnswerB = "an",
                AnswerC = "that",
                AnswerD = "this",
                CorectAns = "a"
            };

            var questionService = new QuestionService(_questionRepository);

            var controller = new QuestionsController(questionService);

            //action
            var result = await controller.AddQuestion(question);

            //course

            result.Should().NotBeNull();

            var createdAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Question>(createdAtActionResult.Value);

            Assert.Equal("This is ... car?", returnValue.Content);
            Assert.Equal("a", returnValue.AnswerA);
            Assert.Equal("an", returnValue.AnswerB);
            Assert.Equal("that", returnValue.AnswerC);
            Assert.Equal("this", returnValue.AnswerD);
            Assert.Equal("a", returnValue.CorectAns);
        }

        [Fact]
        public async Task PostListQuestion_Success()
        {
            var question1 = new QuestionRequest
            {
                Content = "This is ... car?",
                AnswerA = "a",
                AnswerB = "an",
                AnswerC = "that",
                AnswerD = "this",
                CorectAns = "a"
            };

            var question2 = new QuestionRequest
            {
                Content = "What your name?",
                AnswerA = "Mr Nam",
                AnswerB = "a",
                AnswerC = "an",
                AnswerD = "her",
                CorectAns = "Mr Nam"
            };

            var list = new List<QuestionRequest>();
            list.Add(question1);
            list.Add(question2);

            var questionService = new QuestionService(_questionRepository);

            var controller = new QuestionsController(questionService);

            //action
            var result = await controller.PostListQuestions(list);

            //course

            result.Should().NotBeNull();

            var total = await _questionRepository.GetAll();
            total.Should().HaveCount(2);
        }

        [Fact]
        public async Task DeleteQuestion_Success()
        {
            var question1 = new Question
            {
                QuestionId = 1,
                Content = "This is ... car?",
                AnswerA = "a",
                AnswerB = "an",
                AnswerC = "that",
                AnswerD = "this",
                CorectAns = "a"
            };

            var question2 = new Question
            {
                QuestionId =2,
                Content = "What your name?",
                AnswerA = "Mr Nam",
                AnswerB = "a",
                AnswerC = "an",
                AnswerD = "her",
                CorectAns = "Mr Nam"
            };

            var list = new List<Question>();
            list.Add(question1);
            list.Add(question2);

            await _questionRepository.AddRange(list);

            var questionService = new QuestionService(_questionRepository);

            var controller = new QuestionsController(questionService);

            //action
            var result = await controller.DeleteQuestion(1);

            //course

            result.Should().NotBeNull();

            var response = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Question>(response.Value);

            Assert.Equal(1, returnValue.QuestionId);
            Assert.Equal("This is ... car?", returnValue.Content);
            Assert.Equal("a", returnValue.AnswerA);
            Assert.Equal("an", returnValue.AnswerB);
            Assert.Equal("that", returnValue.AnswerC);
            Assert.Equal("this", returnValue.AnswerD);
            Assert.Equal("a", returnValue.CorectAns);

            var totalQuestion = await _questionRepository.GetAll();
            totalQuestion.Should().HaveCount(1);
        }

        [Fact]
        public async Task Update_Success()
        {
            var question = new Question
            {
                QuestionId = 1,
                Content = "This is ... car?",
                AnswerA = "a",
                AnswerB = "an",
                AnswerC = "that",
                AnswerD = "this",
                CorectAns = "a"
            };

            var questionUpdate = new QuestionRequest
            {
                Content = "This is ... car?",
                AnswerA = "my",
                AnswerB = "an",
                AnswerC = "that",
                AnswerD = "this",
                CorectAns = "my"
            };
            await _questionRepository.Add(question);

            var questionService = new QuestionService(_questionRepository);

            var controller = new QuestionsController(questionService);

            //action
            var result = await controller.UpdateQuestion(1,questionUpdate);

            //course

            result.Should().NotBeNull();

            var response = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Question>(response.Value);

            Assert.Equal("This is ... car?", returnValue.Content);
            Assert.Equal("my", returnValue.AnswerA);
            Assert.Equal("an", returnValue.AnswerB);
            Assert.Equal("that", returnValue.AnswerC);
            Assert.Equal("this", returnValue.AnswerD);
            Assert.Equal("my", returnValue.CorectAns);
        }
    }
}
