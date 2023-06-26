using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.OData.Edm;
using Org.BouncyCastle.Asn1.Cms;
using Waterschapshuis.CatchRegistration.External.Api.Features.Catches;
using Waterschapshuis.CatchRegistration.External.Api.Features.Observations;
using Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations;
using Waterschapshuis.CatchRegistration.External.Api.Features.Traps;

// ReSharper disable once CheckNamespace
namespace Waterschapshuis.CatchRegistration.External.Api
{
    public static class ODataStartupExtensions
    {
        public static void AppMapODataRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapODataRoute("odata", "odata", GetEdmModel());
            endpoints.Select().Filter().OrderBy().Count().MaxTop(10000);
        }

        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            // Entities

            builder.EntitySet<GetCatch.CatchItem>("Catches");
            builder.EntitySet<GetTrap.TrapItem>("Traps");
            builder.EntitySet<GetObservation.ObservationItem>("Observations");
            builder.EntitySet<GetTimeRegistration.TimeRegistrationItem>("TimeRegistrations");

            // Functions

            CatchesFunctions(builder);
            TrapsFunctions(builder);
            ObservationsFunctions(builder);
            TimeRegistrationFunctions(builder);

            // Actions

            return builder.GetEdmModel();
        }

        private static void TrapsFunctions(ODataConventionModelBuilder builder)
        {
            var fromYearConfiguration = builder
                .Function("Traps.FromYear")
                .ReturnsCollectionFromEntitySet<GetTrap.TrapItem>("Traps");

            CreateFromYearParameters(fromYearConfiguration);

            var fromOrganizationConfiguration = builder
                .Function("Traps.FromOrganization")
                .ReturnsCollectionFromEntitySet<GetTrap.TrapItem>("Traps");

            CreateFromOrganizationParameters(fromOrganizationConfiguration);
        }
        
        private static void CatchesFunctions(ODataConventionModelBuilder builder)
        {
            var fromYearConfiguration = builder
                .Function(name: "Catches.FromYear")
                .ReturnsCollectionFromEntitySet<GetCatch.CatchItem>("Catches");

            CreateFromYearParameters(fromYearConfiguration);

            var fromOrganizationConfiguration = builder
                .Function(name: "Catches.FromOrganization")
                .ReturnsCollectionFromEntitySet<GetCatch.CatchItem>("Catches");

            CreateFromOrganizationParameters(fromOrganizationConfiguration);
        }

        private static void ObservationsFunctions(ODataConventionModelBuilder builder)
        {
            var fromYearConfiguration = builder
                .Function(name: "Observations.FromYear")
                .ReturnsCollectionFromEntitySet<GetObservation.ObservationItem>("Observations");

            CreateFromYearParameters(fromYearConfiguration);

            var fromOrganizationConfiguration = builder
                .Function(name: "Observations.FromOrganization")
                .ReturnsCollectionFromEntitySet<GetObservation.ObservationItem>("Observations");

            CreateFromOrganizationParameters(fromOrganizationConfiguration);
        }

        private static void TimeRegistrationFunctions(ODataConventionModelBuilder builder)
        {
            var fromYearConfiguration = builder
                .Function("TimeRegistrations.FromYear")
                .ReturnsCollectionFromEntitySet<GetTimeRegistration.TimeRegistrationItem>("TimeRegistrations");

            CreateFromYearParameters(fromYearConfiguration);

            var fromOrganizationConfiguration = builder
                .Function("TimeRegistrations.FromOrganization")
                .ReturnsCollectionFromEntitySet<GetTimeRegistration.TimeRegistrationItem>("TimeRegistrations");

            CreateFromOrganizationParameters(fromOrganizationConfiguration);
        }

        private static void CreateFromYearParameters(FunctionConfiguration configuration)
        {
            configuration.Parameter<int>("Year").Required();
            configuration.Parameter<string>("Organization").Optional();
        }
        private static void CreateFromOrganizationParameters(FunctionConfiguration configuration)
        {
            var organizationConfiguration = configuration.Parameter<string>("Organization");
            organizationConfiguration.Nullable = false;
            organizationConfiguration.Required();

            var yearConfiguration = configuration.Parameter<int>("Year");
            yearConfiguration.Nullable = true;
            yearConfiguration.Optional();
        }
    }
}
