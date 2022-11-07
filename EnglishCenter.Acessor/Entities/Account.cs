using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public bool State { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public int RoleId { get; set; }

        public int CourseId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Course Course { get; set; }

        public virtual List<Result> Results { get; set; }
    }
}
