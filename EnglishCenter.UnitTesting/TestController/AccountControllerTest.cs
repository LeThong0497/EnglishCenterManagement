using EnglisCenter.Accessor;
using EnglisCenter.Business.Services;
using EnglishCenter.Accessor.Entities;
using EnglishCenter.Business.Services;
using EnglishCenter.Common.Models.Account;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EnglishCenter.UnitTesting.TestController
{
    public class AccountControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly EnglishForStudentDB _dbContext;
        private readonly BaseRepository<Account> _accountRepository;
        private readonly JwtAuthenticationManage _jwtAuthenticationManage;

        public AccountControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _dbContext = _fixture.Context;
            _accountRepository = new BaseRepository<Account>(_dbContext);
            _jwtAuthenticationManage = new JwtAuthenticationManage("this is my key to test authentication");
        }

        [Fact]
        public async Task PostAcount_Success()
        {
            var acc = new AccountRequestByAd
            {

              Email =  "string@gmail.com",
              PassWord = "123456",
              State = true,
              FullName = "Nguyen Van A",
              DateOfBirth =DateTime.Parse("2021-06-16"),
              PhoneNumber = 0,
              Gender = "Nam",
              Address = "HCM",
              RoleId = 1,
              CourseId = 1
            };           
        }
    }
}
