using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.Question;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> Add(QuestionRequest questionRequest);

        Task AddRange(List<QuestionRequest> questionRequests);

        Task<Question> GetById(int id);

        Task<IEnumerable<Question>> GetAll();

        Task<Question> Delete(int id);

        Task<Question> Update(int id,QuestionRequest questionRequest);

    }
}
