using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Common.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Account> _baseRepository;

        public AccountService(IBaseRepository<Account> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Account> Add(AccountRequest accountRequest)
        {
            var ac = _baseRepository.Entities.Where(x => x.Email == accountRequest.Email).FirstOrDefault();

            if (ac != null)
            {
                throw new Exception("Exist");
            }

            //Tạo mật khẩu ngẫu nhiên
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var passWord = new String(stringChars);
            //Gửi mail mật khẩu
            SmtpClient smtp = new SmtpClient
            {  
                UseDefaultCredentials=false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("thongle0497@gmail.com", "lethongLT@0497#!"),
                Timeout = 5000,
            };
            MailMessage msg = new MailMessage("thongle0497@gmail.com", accountRequest.Email.ToString().Trim(),
                "Trung tâm Anh Ngữ MEEC",
                "Cảm ơn đã đăng ký học tại trung tâm MEEC. " +
                "\n Tài khoản của bạn sẽ được kích hoạt sau khi thanh toán học phí thành công! ");
            msg.IsBodyHtml = true;

            try
            {
                smtp.Send(msg);
            }
            catch(Exception e)
            {
                throw new Exception("Error"+e);
            }


            //Update mật khẩu vào db          
            var acc = new Account
            {
                FullName = accountRequest.FullName,
                Email = accountRequest.Email,
                CourseId = accountRequest.CourseId,
                PassWord = passWord,
                RoleId = 2,
                State = false
            };

            return await _baseRepository.Add(acc);
        }

        // Add account by admin
        public async Task<Account> AddAcount(AccountRequestByAd accountRequest)
        {
            var ac = _baseRepository.Entities.Where(x => x.Email == accountRequest.Email).FirstOrDefault();

            if (ac != null)
            {
                throw new Exception("Exist");
            }
           
            //Gửi mail mật khẩu
            SmtpClient smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("thongle0497@gmail.com", "lethongLT@0497#!"),
                Timeout = 5000,
            };
            MailMessage msg = new MailMessage("thongle0497@gmail.com", accountRequest.Email.ToString().Trim(),
                "Trung tâm Anh Ngữ MEEC - Cấp mật khẩu",
                "Cảm ơn đã đăng ký học tại trung tâm MEEC. " +
                "\n Mật khẩu đăng nhập của bạn là " + accountRequest.PassWord);
            msg.IsBodyHtml = true;

            try
            {
                smtp.Send(msg);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e);
            }


            //Update mật khẩu vào db          
            var acc = new Account
            {
                FullName = accountRequest.FullName,
                Email = accountRequest.Email,
                CourseId = accountRequest.CourseId,
                PassWord = accountRequest.PassWord,
                RoleId = accountRequest.RoleId,
                State = true,
                DateOfBirth = accountRequest.DateOfBirth,
                Gender = accountRequest.Gender,
                Address = accountRequest.Address,
                PhoneNumber = accountRequest.PhoneNumber,
            };

            return await _baseRepository.Add(acc);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var list = await _baseRepository.GetAll();

            if (list == null)
            {
                return null;
            }
            return list;
        }

        public async Task<Account> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<Account> Update(AccountEditRequest accountEditRequest)
        {
            var acc = await _baseRepository.Entities
                .Where(x => x.Email.Equals(accountEditRequest.Email))
                .FirstOrDefaultAsync();

            if (acc == null)
                return null;
          
            acc.FullName = accountEditRequest.FullName;
            acc.DateOfBirth = accountEditRequest.DateOfBirth;
            acc.PhoneNumber = accountEditRequest.PhoneNumber;
            acc.Gender = accountEditRequest.Gender;
            acc.Address = accountEditRequest.Address;
            acc.CourseId = accountEditRequest.CourseId;

            return await _baseRepository.Update(acc);
        }
       
        public async Task<bool> ForgetPassWord(string email)
        {
            var acc =await _baseRepository.Entities.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();

            if (acc == null)
                return false;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("thongle0497@gmail.com", "lethongLT@0497#!"),
                Timeout = 5000,
            };
            MailMessage msg = new MailMessage("thongle0497@gmail.com", acc.Email.ToString().Trim(),
                "Trung tâm Anh Ngữ MEEC - Cấp lại mật khẩu",
                "Mật khẩu cũ của bạn là: " + acc.PassWord.ToString() + "\n Chúc bạn đạt kết quả tốt!");
            msg.IsBodyHtml = true;

            try
            {
                smtp.Send(msg);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e);
            }

            return true;
        }

        public async Task<bool> ChangePassword(int id,AccountChangePassword account )
        {
            var acc =await _baseRepository.GetById(id);

            if (acc == null)
                throw new Exception("Not Found");

            if(!acc.PassWord.Equals(account.OldPassWord))
            {
                return false;
            }

            acc.PassWord = account.NewPassWord;

            await  _baseRepository.Update(acc);

            return true;
        }

        public async Task<IEnumerable<Account>> GetAccountByCourse(int id)
        {
            var accounts =await _baseRepository.Entities.Where(x => x.CourseId.Equals(id)).ToListAsync();

            if (accounts == null)
                return null;

            return accounts;
        }

        public async Task<Account> ActiveAccout(int id)
        {
            var acc = await _baseRepository.Entities.Where(x => x.AccountId.Equals(id)).FirstOrDefaultAsync();

            if (acc == null)
                throw new Exception("Not Found");

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("thongle0497@gmail.com", "lethongLT@0497#!"),
                Timeout = 5000,
            };
            MailMessage msg = new MailMessage("thongle0497@gmail.com", acc.Email.ToString().Trim(),
                "Trung tâm Anh Ngữ MEEC - Cấp mật khẩu",
                "Mật khẩu của bạn là: " + acc.PassWord.ToString() + "\n Chúc bạn đạt kết quả tốt!");
            msg.IsBodyHtml = true;

            try
            {
                smtp.Send(msg);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e);
            }

            acc.State = true;
            await _baseRepository.Update(acc);

            return acc;
        }
    }
}
