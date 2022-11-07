using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IBaseRepository<Question> _baseRepository;

        public QuestionService(IBaseRepository<Question> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Question> Add(QuestionRequest questionRequest)
        {
            var question = new Question
            {
                Content = questionRequest.Content,
                AnswerA = questionRequest.AnswerA,
                AnswerB = questionRequest.AnswerB,
                AnswerC = questionRequest.AnswerC,
                AnswerD = questionRequest.AnswerD,
                CorectAns = questionRequest.CorectAns
            };

            return await _baseRepository.Add(question);
        }

        public  Task AddRange(List<QuestionRequest> questionRequests)
        {
            var listQuestions = new List<Question>();

            for(int i=0;i<questionRequests.Count();i++)
            {
                var question = new Question
                {
                    Content = questionRequests[i].Content,
                    AnswerA = questionRequests[i].AnswerA,
                    AnswerB = questionRequests[i].AnswerB,
                    AnswerC = questionRequests[i].AnswerC,
                    AnswerD = questionRequests[i].AnswerD,
                    CorectAns = questionRequests[i].CorectAns
                };

                listQuestions.Add(question);
            }

         return _baseRepository.AddRange(listQuestions);

        }

        public async Task<Question> Delete(int id)
        {
            var question =await _baseRepository.GetById(id);

            if(question == null)
            {
                throw new Exception("Not Found");
            }

            return await _baseRepository.Delete(question);
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Question> GetById(int id)
        {
            var question = await _baseRepository.GetById(id);

            if (question == null)
            {
                throw new Exception("Not Found");
            }

            return question;
        }

        public async Task<Question> Update(int id,QuestionRequest questionRequest)
        {
            var question = await _baseRepository.GetById(id);

            if (question == null)
            {
                throw new Exception("Not Found");
            }

            question.AnswerA = questionRequest.AnswerA;
            question.AnswerB = questionRequest.AnswerB;
            question.AnswerC = questionRequest.AnswerC;
            question.AnswerD = questionRequest.AnswerD;
            question.CorectAns = questionRequest.CorectAns;

            return await _baseRepository.Update(question);
        }
    }
}
