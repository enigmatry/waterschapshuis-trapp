using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class Organization : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 50;
        public const int ShortNameMaxLength = 10;
        
        private readonly List<Rayon> _rayons = new List<Rayon>();
        private readonly List<WaterAuthority> _waterAuthorities = new List<WaterAuthority>();
        private readonly List<User> _users = new List<User>();

        private Organization() { }

        public string Name { get; private set; } = String.Empty;
        public string ShortName { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public IReadOnlyCollection<Rayon> Rayons => _rayons.AsReadOnly();
        public IReadOnlyCollection<WaterAuthority> WaterAuthorities => _waterAuthorities.AsReadOnly();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();


        public static Organization Create(string name, string shortName, Geometry geometry)
        {
            return new Organization
            {
                Id = GenerateId(),
                Name = name,
                ShortName = shortName,
                Geometry = geometry
            };
        }

        public Organization AddRayon(Rayon rayon)
        {
            _rayons.Add(rayon);
            return this;
        }

        public Organization AddWaterAuthority(WaterAuthority waterAuthority)
        {
            _waterAuthorities.Add(waterAuthority);
            return this;
        }
    }
}
