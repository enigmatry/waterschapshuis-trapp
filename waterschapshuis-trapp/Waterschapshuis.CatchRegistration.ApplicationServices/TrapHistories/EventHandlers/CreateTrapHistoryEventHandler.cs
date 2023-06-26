using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TrapHistories.EventHandlers
{
    [UsedImplicitly]
    public class CreateTrapHistoryEventHandler : INotificationHandler<TrapHistoryDomainEvent>
    {
        private readonly IRepository<TrapHistory> _trapHistoryRepository;

        public CreateTrapHistoryEventHandler(IRepository<TrapHistory> trapHistoryRepository)
        {
            _trapHistoryRepository = trapHistoryRepository;
        }

        public Task Handle(TrapHistoryDomainEvent notification, CancellationToken cancellationToken)
        {
            _trapHistoryRepository.AddRange(TrapHistory.Create(notification));
            return Task.CompletedTask;
        }
    }
}
