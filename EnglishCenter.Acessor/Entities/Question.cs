using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{ 
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string Content { get; set; }

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC { get; set; }

        public string AnswerD { get; set; }

        public string CorectAns { get; set; }

    }
}
