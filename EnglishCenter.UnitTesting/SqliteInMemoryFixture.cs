using EnglisCenter.Accessor;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnglishCenter.UnitTesting
{
    public class SqliteInMemoryFixture : IDisposable
    {
        private IServiceScope _serviceScope;
        private SqliteConnection _connection;

        public virtual IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceScope == null)
                {
                    _serviceScope = ConfigureServices(new ServiceCollection()).BuildServiceProvider().CreateScope();
                }

                return _serviceScope.ServiceProvider;
            }
        }

        public virtual EnglishForStudentDB Context => ServiceProvider.GetRequiredService<EnglishForStudentDB>();

        public virtual void CreateDatabase()
        {
            Dispose();
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            Context.Database.EnsureCreated();
        }

        public virtual void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _serviceScope?.Dispose();
            _serviceScope = null;
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EnglishForStudentDB>(b => b.UseSqlite(_connection));
          
            services.AddLogging();

            return services;
        }
    }
}
