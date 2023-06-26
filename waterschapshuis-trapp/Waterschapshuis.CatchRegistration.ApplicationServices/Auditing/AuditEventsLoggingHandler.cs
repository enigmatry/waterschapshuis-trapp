using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Auditing
{
    // when renaming this class don't forget to rename the log filter configuration in the settings file
    [UsedImplicitly]
    public class AuditEventsLoggingHandler<T> : INotificationHandler<T> where T : AuditableDomainEvent
    {
        private readonly ILogger<AuditEventsLoggingHandler<T>> _log;
        private readonly ICurrentUserProvider _currentUserProvider;

        public AuditEventsLoggingHandler(ILogger<AuditEventsLoggingHandler<T>> log, ICurrentUserProvider currentUserProvider)
        {
            _log = log;
            _currentUserProvider = currentUserProvider;
        }

        public Task Handle(T notification, CancellationToken cancellationToken)
        {
            if (notification.AuditEnabled)
            {
                _log.LogInformation("Event name: {EventName}, Payload: {@Payload}, initiated by: {Email}", notification.EventName, notification.AuditPayload, _currentUserProvider.Email);
            }
            return Task.CompletedTask;
        }
    }
}
