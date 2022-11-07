using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.MailBox;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
   public class MailBoxService : IMailBoxService
    {
        private readonly IBaseRepository<MailBox> _baseRepository;

        public MailBoxService(IBaseRepository<MailBox> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<MailBox> AddAsync(MailBoxRequest mailBoxRequest)
        {
            var mailBox = new MailBox
            {
                FullName=mailBoxRequest.FullName,
                Email=mailBoxRequest.Email,
                Phone=mailBoxRequest.Phone,
                Content=mailBoxRequest.Content,
                Date=DateTime.Now
            };

            var result = await _baseRepository.Add(mailBox);

            return result;
        }

        public async Task<IEnumerable<MailBox>> GetAllAsync()
        {
            var mailBoxs =await _baseRepository.Entities.OrderByDescending(x=>x.Id).ToListAsync();
            
            if (mailBoxs == null)
                return null;

            return mailBoxs;
        }
    }
}
