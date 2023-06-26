using System;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Anonymization
{
    public interface IAnonymizeCreatedUpdatedBy : IAnonymizeCreatedBy
    {
        DateTimeOffset UpdatedOn { get; }
        Guid UpdatedById { get; }
        User UpdatedBy { get; }
    }
}
