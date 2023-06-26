using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public static partial class GetTimeRegistrationPerRayon
    {

        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public DateTimeOffset StartDate { get; set; }
            public DateTimeOffset EndDate { get; set; }


            public static Query Create(DateTimeOffset startDate, DateTimeOffset endDate)
            {
                return new Query { StartDate = startDate, EndDate = endDate };
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// List of users who registered time in a rayon
            /// </summary>
            public IEnumerable<TimeRegistrationsPerRayon> UsersWithRegisteredTimePerRayon { get; set; } = Enumerable.Empty<TimeRegistrationsPerRayon>();

            /// <summary>
            /// List of users who made time registration generals
            /// </summary>
            public IEnumerable<TimeRegistrationsPerRayon.User> UsersWithTimeRegistrationGeneralItems { get; set; } = Enumerable.Empty<TimeRegistrationsPerRayon.User>();

            /// <summary>
            /// List of users in the organization rayon belongs to
            /// </summary>
            public IEnumerable<TimeRegistrationsPerRayon.User> UsersInOrganization { get; set; } = Enumerable.Empty<TimeRegistrationsPerRayon.User>();

            public class TimeRegistrationsPerRayon
            {
                /// <summary>
                /// GUID of the rayon
                /// </summary>
                public Guid RayonId { get; set; }

                /// <summary>
                /// Name of the Rayon
                /// </summary>
                public string RayonName { get; set; } = String.Empty;

                /// <summary>
                /// List of user in this rayon
                /// </summary>
                public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();

                public class User
                {
                    /// <summary>
                    /// GUID of user
                    /// </summary>
                    public Guid Id { get; set; }

                    /// <summary>
                    /// Fullname of user
                    /// </summary>
                    public string Name { get; set; } = null!;

                    /// <summary>
                    /// Indication whether user closed the week for time writing
                    /// </summary>
                    public bool WeekCompleted { get; set; }

                    /// <summary>
                    /// Indication whether time can be entered for this week
                    /// </summary>
                    public bool WeekActive { get; set; }

                }
            }
        }

        private static IQueryable<Catch> BuildInclude(this IQueryable<Catch> query)
        {
            return query.Include(x => x.CreatedBy)
                .Include(x => x.Trap)
                .ThenInclude(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon);
        }

        private static IQueryable<DomainModel.TimeRegistrations.TimeRegistration> BuildInclude(this IQueryable<DomainModel.TimeRegistrations.TimeRegistration> query)
        {
            return query.Include(x => x.User)
                        .Include(x => x.SubAreaHourSquare)
                        .ThenInclude(x => x.SubArea)
                        .ThenInclude(x => x.CatchArea)
                        .ThenInclude(x => x.Rayon);
        }

        private static IQueryable<TimeRegistrationGeneral> BuildInclude(this IQueryable<TimeRegistrationGeneral> query)
        {
            return query.Include(x => x.User);
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, Response.TimeRegistrationsPerRayon.User>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            }
        }
    }
}
