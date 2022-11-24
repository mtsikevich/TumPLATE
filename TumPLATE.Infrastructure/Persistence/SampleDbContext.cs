using Microsoft.EntityFrameworkCore;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Infrastructure.Persistence;

public class SampleDbContext: DbContext
{
    public DbSet<Tree> Trees { get; set; }
    
    public SampleDbContext(DbContextOptions options) 
        : base(options)
    {
    }
}