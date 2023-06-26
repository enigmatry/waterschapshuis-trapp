using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(rp => new {rp.UserId, rp.RoleId});
        }
    }
}
