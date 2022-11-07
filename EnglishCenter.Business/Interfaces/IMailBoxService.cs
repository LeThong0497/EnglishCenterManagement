using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.MailBox;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface IMailBoxService
    {
        Task<IEnumerable<MailBox>> GetAllAsync();

        Task<MailBox> AddAsync(MailBoxRequest mailBoxRequest);
    }
}
