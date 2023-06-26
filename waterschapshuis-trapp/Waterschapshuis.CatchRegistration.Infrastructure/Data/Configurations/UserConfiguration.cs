using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(User.TextualFieldMaxLength);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(User.TextualFieldMaxLength);

            builder.Property(x => x.Surname)
                .HasMaxLength(User.TextualFieldMaxLength);

            builder.Property(x => x.GivenName)
                .HasMaxLength(User.TextualFieldMaxLength);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.CreatedUsers).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedUsers).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedTraps).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedTraps).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedCatches).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedCatches).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedTrackings).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedTrackings).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedObservations).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedObservations).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CreatedTimeRegistrations).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedTimeRegistrations).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedTimeRegistrationsGeneral).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedTimeRegistrationsGeneral).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CreatedTrapHistories).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UpdatedTrapHistories).WithOne(x => x.UpdatedBy).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.UserSessions).WithOne(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
