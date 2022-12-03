using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Domain.Tree;
using TumPLATE.Infrastructure.Persistence.TreePersistence;

namespace TumPLATE.Infrastructure.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddSqlServerPersistence(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "The connection string is not provided");
            
            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddDbContext<SampleDbContext>(options =>
            
                options.UseSqlServer(connectionString)
            );
        }

        public static void ApplyDataMigrations<TD>(this IApplicationBuilder app) 
            where TD: DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TD>();

            if (dbContext is null)
                throw new Exception("DbContext not found");
            
            dbContext.Database.Migrate();
        }
    }
}
