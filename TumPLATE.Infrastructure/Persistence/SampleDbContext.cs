using Microsoft.EntityFrameworkCore;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Infrastructure.Persistence;

public class SampleDbContext: DbContext
{
    public DbSet<TreeState> Trees { get; set; }
    
    public SampleDbContext(DbContextOptions<SampleDbContext> options) 
        : base(options)
    {
    }
}