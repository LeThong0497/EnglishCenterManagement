using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Account;
using EnglishCenter.Common.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglisCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtAuthenticationManage _jwtAuthenticationManage;
        public AccountController(IAccountService accountService,IJwtAuthenticationManage jwtAuthenticationManage)
        {
            _accountService = accountService;
            _jwtAuthenticationManage = jwtAuthenticationManage;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> SignUp(AccountRequest account)
        {
            try
            {
                await _accountService.Add(account);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return Content("Success");
        }

        [HttpPost("admin/add")]
        public async Task<ActionResult<Account>> AddAcount(AccountRequestByAd account)
        {
            try
            {
                await _accountService.AddAcount(account);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return Content("Success");
        }
        
        /* [HttpPost("authenticate")]
         public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
         {
             if(!(await _accountService.GetAll()).Any(x=>x.Email.Equals(userLogin.Email) && x.PassWord.Equals(userLogin.PassWord)))
             {
                 throw new Exception("Email or PassWord is incorrect");
             }

             var acc = (await _accountService.GetAll()).Where(x => x.Email.Equals(userLogin.Email))
                 .FirstOrDefault();
             var token = _jwtAuthenticationManage.Authenticate(userLogin.Email, userLogin.PassWord);
             if (token == null)
                 return Unauthorized();
             return Ok(token);
         }*/

        [HttpPost("login")]
        
        public async Task<ActionResult<AccountResponse>> GetInfAcc([FromBody] UserLogin userLogin)
        {
            if (!(await _accountService.GetAll()).Any(x => x.Email.Equals(userLogin.Email) && x.PassWord.Equals(userLogin.PassWord)))
            {
                return Content("Email or password incorrect!");
            }

            var acc = (await _accountService.GetAll()).Where(x => x.Email.Equals(userLogin.Email))
                .FirstOrDefault();

            var accresponse = new AccountResponse
            {
                AccountId = acc.AccountId,
                Email = acc.Email,
                FullName = acc.FullName,
                DateOfBirth = acc.DateOfBirth,
                PhoneNumber = acc.PhoneNumber,
                Gender = acc.Gender,
                Address = acc.Address,
                CourseId = acc.CourseId,
                RoleId = acc.RoleId
            };
            return Ok(accresponse);
        }

        [HttpGet]       
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
          var list= await _accountService.GetAll();
           return Ok(list);
        }

        [HttpPut]
       
        public async Task<ActionResult<Account>> Update([FromBody]AccountEditRequest accountEditRequest)
        {
            var acc = await _accountService.Update(accountEditRequest);
            if (acc != null)
                return StatusCode(200);
            else
                throw new Exception("Update failed");
        }

        [HttpGet("getpassword")] 
        public async Task<ActionResult> ForgetPassword(string email)
        {
            var result =await _accountService.ForgetPassWord(email);

            if (result == false)
                return Content("Email not found");
            return Content("Success");

        }

        [HttpPut("changepw/{accid}")]
        public async Task<ActionResult> ChangePassword([FromRoute] int accid, AccountChangePassword account)
        {
            var result = await _accountService.ChangePassword(accid, account);

            if (result)
                return Ok("Success");
            return Content("WrongPassword");
        }

        [HttpGet("course/{id}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountByCourse([FromRoute]int id)
        {
            var list = await _accountService.GetAccountByCourse(id);

            if (list == null)
                return StatusCode(404);

            return Ok(list);
        }

        [HttpGet("active/{id}")]
        public async Task<ActionResult<Account>> ActiveAccount([FromRoute] int id)
        {
            var ac = await _accountService.ActiveAccout(id);

            if (ac == null)
                return StatusCode(404);

            return Ok(ac);
        }
    }
}
