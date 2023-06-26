using System;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Anonymization
{
    public interface IAnonymizeCreatedBy
    {
        DateTimeOffset CreatedOn { get; }
        Guid CreatedById { get; }
        User CreatedBy { get; }
        Guid? LocationOrganizationId { get; }
    }
}
