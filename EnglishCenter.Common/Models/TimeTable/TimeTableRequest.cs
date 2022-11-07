using System;

namespace EnglishCenter.Common.Models.TimeTable
{
    public class TimeTableRequest
    {
        public int CourseId { get; set; }

        public int IdZoom { get; set; }

        public int PassZoom { get; set; }

        public int Date { get; set; } // 0 to 6 : monday to sunday

        public int Time { get; set; } // 0 to 6 

        public DateTime DateStart { get; set; }

        public int AccountId { get; set; }
    }
}
