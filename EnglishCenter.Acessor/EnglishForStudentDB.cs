using EnglishCenter.Accessor.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglisCenter.Accessor
{
    public class EnglishForStudentDB : DbContext
    {       
          public EnglishForStudentDB(DbContextOptions<EnglishForStudentDB> options) : base(options)
            {
              
            }

            public DbSet<Course> Courses { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Test> Tests { get; set; }
            public DbSet<TestDetail> TestDetails { get; set; }
            public DbSet<Account> Accounts { get; set; }
            public DbSet<Result> Results { get; set; }
            public DbSet<DetailResult> DetailResults { get; set; }
            public DbSet<MailBox> MailBoxes { get; set; }
            public DbSet<TimeTable> TimeTables { get; set; }
    }
}
