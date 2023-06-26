using System;
using AutoMapper;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    public static class NamedEntity
    {
        [PublicAPI]
        public class Item
        {
            public Guid Id { get; set; } = default!;
            public string Name { get; set; } = String.Empty;

            public static Item Create(IEntityHasName<Guid> entity)
            {
                return new Item
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<IEntityHasName<Guid>, Item>();
            }
        }
    }
}
