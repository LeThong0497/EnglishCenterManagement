namespace EnglishCenter.Common.Models.ResultDetail
{
    public class ResultDetailRequest
    {
        public int ResultId { get; set; }
        public string SelectedAns { get; set; }
        public int QuestionId { get; set; }
        public bool Ok { get; set; }
    }
}
