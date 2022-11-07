using EnglishCenter.Common.Models.ResultDetail;
using System;
using System.Collections.Generic;

namespace EnglishCenter.Common.Models.Result
{
    public class ResultDetailResponse
    {        
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public  List<ResultDetailRequest> DetailResults { get; set; }
    }
}
