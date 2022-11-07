using EnglisCenter.Business.Services;
using EnglishCenter.Business.Interfaces;
using EnglishCenter.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Extensions.ServiceCollection
{
    public static class ServcieRegister
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ITestDetailService, TestDetailService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IResultDetailService, ResultDetailService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IMailBoxService, MailBoxService>();
            services.AddTransient<ITimeTableService, TimeTableService>();

        }
    }
}