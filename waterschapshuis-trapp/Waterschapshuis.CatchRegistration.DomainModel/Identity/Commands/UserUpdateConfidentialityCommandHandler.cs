using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    [UsedImplicitly]
    public class UserUpdateConfidentialityCommandHandler : IRequestHandler<UserUpdateConfidentiality.Command, UserUpdateConfidentiality.Result>
    {
        private readonly IRepository<User> _userRepository;

        public UserUpdateConfidentialityCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserUpdateConfidentiality.Result> Handle(UserUpdateConfidentiality.Command request, CancellationToken cancellationToken)
        {
            User user = await _userRepository
                .QueryAll()
                .Include(u => u.UserRoles)
                .QueryById(request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            user.UpdateConfidentialityConfirmed(request);

            var result = UserUpdateConfidentiality.Result.CreateResult(user.Id);
            return result;
        }
    }
}
