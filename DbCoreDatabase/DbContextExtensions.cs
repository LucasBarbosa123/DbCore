using DbCoreDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbCoreDatabase
{
    public static class DbContextExtensions
    {
        public static void AddOCPPDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DbCore;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<DbCoreContext>(opt => opt.UseSqlServer(connectionString));
        }
    }
}
