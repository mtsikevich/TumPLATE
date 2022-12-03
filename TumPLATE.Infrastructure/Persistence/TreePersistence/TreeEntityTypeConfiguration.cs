using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TumPLATE.Domain.Tree;

namespace TumPLATE.Infrastructure.Persistence.TreePersistence;

public class TreeEntityTypeConfiguration:IEntityTypeConfiguration<TreeState>
{
    public void Configure(EntityTypeBuilder<TreeState> builder)
    {
        builder.Property(t => t.Name).IsRequired();
    }
}