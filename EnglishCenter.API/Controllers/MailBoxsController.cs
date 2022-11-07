using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.MailBox;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailBoxsController : Controller
    {
        private readonly IMailBoxService _mailBoxService;

        public MailBoxsController(IMailBoxService mailBoxService)
        {
            _mailBoxService = mailBoxService;
        }

        [HttpPost]
        public async Task<ActionResult<MailBox>> AddAsync(MailBoxRequest mailBoxRequest)
        {
            var mail = await _mailBoxService.AddAsync(mailBoxRequest);

            return Ok(mail);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailBox>>> GetAllAsync()
        {
            var lists = await _mailBoxService.GetAllAsync();

            return Ok(lists);
        }
    }
}
