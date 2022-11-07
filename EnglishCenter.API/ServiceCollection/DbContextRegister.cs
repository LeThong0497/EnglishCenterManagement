using EnglisCenter.Accessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Extensions.ServiceCollection
{
    public static class DbContextRegister
    {
        public static void AddDbContextRegister(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EnglishForStudentDB>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }
    }
}