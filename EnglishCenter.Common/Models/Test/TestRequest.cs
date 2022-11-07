using System;

namespace EnglishCenter.Common.Models.Test
{
    public class TestRequest
    {
        public string TestName { get; set; }
        public string Createsby { get; set; }
        public int CourseId { get; set; }
        public DateTime DateTest { get; set; }
        public DateTime DateCreated { get; set; }
        public int Time { get; set; } // minute
        public int TotalQuestion { get; set; }
    }
}
