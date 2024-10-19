using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Entities;

namespace TaskManager.Persistence.EntitiesConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<Entities.Task>
{
    public void Configure(EntityTypeBuilder<Entities.Task> builder)
    {
        builder.Property(x=> x.Name).HasMaxLength(100); ;
        builder.Property(x=> x.Description).HasMaxLength(2500); ;
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
