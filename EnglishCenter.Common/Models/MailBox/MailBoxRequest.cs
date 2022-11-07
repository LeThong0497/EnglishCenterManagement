using System;

namespace EnglishCenter.Common.Models.MailBox
{
    public class MailBoxRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Content { get; set; }
    }
}
