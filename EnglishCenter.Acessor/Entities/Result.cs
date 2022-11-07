using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        public int AccountId { get; set; }
        public int TestId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public virtual IList<DetailResult> DetailResults { get; set; }
        public virtual Account Account { get; set; }
        public virtual Test Test { get; set; }
    }
}

