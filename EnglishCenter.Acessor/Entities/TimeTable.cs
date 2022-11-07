using System;
using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{
    public class TimeTable
    {
        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int IdZoom { get; set; }

        public int PassZoom { get; set; }

        public int Date { get; set; } // 0 to 6 : monday to sunday

        public int Time { get; set; } // 0 to 6 

        public int AccountId { get; set; }

        public DateTime DateStart { get; set; }

        public virtual Account Account { get; set; }

        public virtual Course Course { get; set; }
    }
}
