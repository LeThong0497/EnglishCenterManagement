using System;

namespace EnglishCenter.Common.Models.Account
{
    public  class AccountEditRequest
    {
        public string Email { get; set; }      
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }      
        public int CourseId { get; set; }
    }
}
