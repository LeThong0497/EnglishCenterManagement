using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCenter.Common.Models.Result
{
   public class ResultResponse
    {
        public string FullName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }        
    }
}
