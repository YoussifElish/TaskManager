using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Entities;

namespace TaskManager.Persistence.EntitiesConfiguration;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.Property(x=> x.Name).HasMaxLength(100); 
        builder.Property(x=> x.Email).HasMaxLength(50);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
