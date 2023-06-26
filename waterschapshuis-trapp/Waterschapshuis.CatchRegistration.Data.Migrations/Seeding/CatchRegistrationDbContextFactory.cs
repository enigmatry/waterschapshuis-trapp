using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Infrastructure;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Infrastructure.MediatR;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding
{
    [UsedImplicitly]
    public class CatchRegistrationDbContextFactory : IDesignTimeDbContextFactory<CatchRegistrationDbContext>
    {
        private const string DevelopmentConnectionString = "Server=.;Database=waterschapshuis-catch-registration;Trusted_Connection=True;MultipleActiveResultSets=true";

        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        public CatchRegistrationDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext(ReadConnectionString());
        }

        public CatchRegistrationDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatchRegistrationDbContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b =>
                {
                    b.MigrationsAssembly("Waterschapshuis.CatchRegistration.Data.Migrations");
                    b.UseNetTopologySuite();
                    b.CommandTimeout((int)TimeSpan.FromMinutes(15).TotalSeconds);
                });

            var result =
                new CatchRegistrationDbContext(optionsBuilder.Options, new NoMediator(), new TimeProvider(), new NullCurrentUserIdProvider(), new NullLogger<CatchRegistrationDbContext>(), new NullDbContextAccessTokenProvider())
                {
                    ModelBuilderConfigurator = DbInitializer.SeedData
                };
            return result;
        }

        // reading Environment variables because arguments cannot be passed in
        // https://github.com/aspnet/EntityFrameworkCore/issues/8332
        private static string ReadConnectionString()
        {
            const string environmentVariableName = "MigrateDatabaseConnectionString";
            string? connectionString = Environment.GetEnvironmentVariable(environmentVariableName);
            Console.WriteLine(String.IsNullOrEmpty(connectionString)
                ? $"{environmentVariableName} environment variable is not set."
                : $"{environmentVariableName} variable was found. ");

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine($"Falling back to developers connection string: '{DevelopmentConnectionString}'");
                return DevelopmentConnectionString;
            }

            return connectionString;
        }

        private class NullDbContextAccessTokenProvider : IDbContextAccessTokenProvider
        {
            public Task<string> GetAccessTokenAsync()
            {
                return Task.FromResult(String.Empty);
            }
        }

        private class NullCurrentUserIdProvider : ICurrentUserIdProvider
        {
            public Guid? FindUserId(IQueryable<User> query)
            {
                return null;
            }

            public string Email => String.Empty;
            public bool IsAuthenticated => false;
        }
    }
}
