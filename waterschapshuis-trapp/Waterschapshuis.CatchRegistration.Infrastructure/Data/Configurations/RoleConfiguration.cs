using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();;

            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.UserRoles).WithOne(x => x.Role).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
