using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(x => x.AccessTokens).WithOne(x => x.UserSession).OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(x => x.UpdatedById);
            builder.Ignore(x => x.UpdatedOn);
        }
    }
}
