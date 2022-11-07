using EnglishCenter.Common.Models.Question;
using System;
using System.Collections.Generic;

namespace EnglishCenter.Common.Models.Test
{
    public class TestResponse
    {
        public int TestId { get; set; }

        public string TestName { get; set; }

        public DateTime DateCreated { get; set; }

        public int Time { get; set; } // minute

        public int CourseId { get; set; }

        public bool State { get; set; } // false : Đã thi ; true : Chưa thi

        public string Createsby { get; set; }

        public int TotalQuestion { get; set; }

        public DateTime DateTest { get; set; }

        public List<QuestionResponse> ListQuestions { get; set; }
    }
}
