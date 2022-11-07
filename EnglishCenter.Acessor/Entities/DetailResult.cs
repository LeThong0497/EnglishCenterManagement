using System.ComponentModel.DataAnnotations;

namespace EnglishCenter.Accessor.Entities
{ 
    public class DetailResult
    {
        [Key]
        public int Id { get; set; }

        public int ResultId { get; set; }

        public int QuestionId { get; set; }

        public string SelectedAns { get; set; }

        public bool Ok { get; set; }

    }
}
