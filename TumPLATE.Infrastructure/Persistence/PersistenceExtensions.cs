using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Domain.Tree;
using TumPLATE.Infrastructure.Persistence.TreePersistence;

namespace TumPLATE.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static void AddSqlPersistence(this IServiceCollection services, string? connectionString, DbVendor dbVendor = DbVendor.SqlServer)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString), "The connection string is not provided");
            
        services.AddScoped<ITreeRepository, TreeRepository>();
        services.AddDbContext<SampleDbContext>(options =>
        {
            switch (dbVendor)
            {
                case DbVendor.SqlServer:
                    options.UseSqlServer(connectionString);
                    break;
                case DbVendor.Postgres:
                    options.UseNpgsql(connectionString);
                    break;
                default:
                    break;
            }
        });
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