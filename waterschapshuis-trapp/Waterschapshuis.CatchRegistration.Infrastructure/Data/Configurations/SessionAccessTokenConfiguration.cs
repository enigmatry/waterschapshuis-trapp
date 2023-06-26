using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class SessionAccessTokenConfiguration : IEntityTypeConfiguration<SessionAccessToken>
    {
        public void Configure(EntityTypeBuilder<SessionAccessToken> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
