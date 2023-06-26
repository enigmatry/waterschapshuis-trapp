using JetBrains.Annotations;
using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    [UsedImplicitly]
    public class AutoCreateUserAfterLoginCommandHandler : IRequestHandler<AutoCreateUserAfterLogin.Command, AutoCreateUserAfterLogin.Result>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITimeProvider _timeProvider;

        public AutoCreateUserAfterLoginCommandHandler(
            IRepository<User> userRepository,
            ITimeProvider timeProvider)
        {
            _userRepository = userRepository;
            _timeProvider = timeProvider;
        }

        public async Task<AutoCreateUserAfterLogin.Result> Handle(AutoCreateUserAfterLogin.Command request, CancellationToken cancellationToken)
        {
            var result = new AutoCreateUserAfterLogin.Result();
            var user = await _userRepository.QueryAll().GetByEmailWithSessions(request.Email, cancellationToken);

            if (user == null)
            {
                Log.Information("User not found in db, creating new: ${Email}", request.Email);
                user = User.Create(request);
                user.SetCreated(_timeProvider.Now, user.Id);
                user.SetUpdated(_timeProvider.Now, user.Id);
                _userRepository.Add(user);
                result.Created = true;
            }

            result.UserId = user.Id;

            return result;
        }
    }
}
