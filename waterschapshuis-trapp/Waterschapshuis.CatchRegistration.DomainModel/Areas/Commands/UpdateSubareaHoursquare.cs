using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas.Commands
{
    public static partial class UpdateSubareaHoursquare
    {
        [PublicAPI]
        public class Command : IRequest<ScheduledJobExecute.Result> { }
    }
}
