using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCenter.Common.Models.Account
{
    public class AccountResponse
    {
        public int AccountId { get; set; }

        public string Email { get; set; }
       
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public int RoleId { get; set; }

        public int CourseId { get; set; }
    }
}
