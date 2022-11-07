using EnglishCenter.Accessor.Entities;
using EnglishCenter.Common.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface IAccountService
    {
        Task<Account> Add(AccountRequest accountRequest);

        Task<Account> AddAcount(AccountRequestByAd accountRequest);

        Task<Account> GetById(int id);

        Task<IEnumerable<Account>> GetAll();

        Task<Account> Update(AccountEditRequest accountEditRequest);

        Task<bool> ForgetPassWord(string email);

        Task<bool> ChangePassword(int id,AccountChangePassword account);

        Task<IEnumerable<Account>> GetAccountByCourse(int id);

        Task<Account> ActiveAccout(int id);

    }
}
