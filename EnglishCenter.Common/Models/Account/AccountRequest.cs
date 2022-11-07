namespace EnglishCenter.Common.Models.Account
{
    public class AccountRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CourseId { get; set; }
        public int Role { get; set; }
    }
}
