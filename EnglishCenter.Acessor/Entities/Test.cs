using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }

        [StringLength(maximumLength:30)]
        public string TestName { get; set; }

        public DateTime DateCreated { get; set; }

        public int Time { get; set; } // minute

        public int CourseId { get; set; }

        [DefaultValue(true)]
        public bool State { get; set; } // false : Đã thi ; true : Chưa thi

        public string Createsby { get; set; }

        public int TotalQuestion { get; set; }

        public DateTime DateTest { get; set; }

        public virtual List<TestDetail> TestDetails { get; set; }

        public virtual List<Result> Results { get; set; }
       
    }
}
