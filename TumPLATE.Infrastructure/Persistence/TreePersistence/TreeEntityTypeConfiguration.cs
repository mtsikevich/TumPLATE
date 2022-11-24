using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Infrastructure.Persistence.TreePersistence;

public class TreeEntityTypeConfiguration:IEntityTypeConfiguration<Tree>
{
    public void Configure(EntityTypeBuilder<Tree> builder)
    {
        builder.ToTable("");
    }
}