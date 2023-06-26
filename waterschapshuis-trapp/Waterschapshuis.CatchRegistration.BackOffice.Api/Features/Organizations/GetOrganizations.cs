using System;
using System.Collections.Generic;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Organizations
{
    public static partial class GetOrganizations
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<Item> Items { get; set; } = new List<Item>();
             
            [PublicAPI]
            public class Item
            {
                /// <summary>
                /// GUID of the organization
                /// </summary>
                public Guid Id { get; set; }
                
                /// <summary>
                /// Name of the organization
                /// </summary>
                public string Name { get; set; } = String.Empty;
            }

            [UsedImplicitly]
            public class MappingProfile : Profile
            {
                public MappingProfile() => CreateMap<Organization, Item>();
            }
        }

    }
}
