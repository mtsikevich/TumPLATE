using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TumPLATE.Domain.Tree;
using TumPLATE.Infrastructure.Persistence.TreePersistence;

namespace TumPLATE.Infrastructure.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddDbContext<SampleDbContext>(options =>
            
                options.UseSqlServer(configuration.GetConnectionString("sampleDb"))
            );
        }
    }
}
