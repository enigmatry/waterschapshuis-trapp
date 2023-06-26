using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Accounts
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AccountController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        
        /// <summary>
        ///     Gets profile of the user
        /// </summary>
        /// <returns>User profile as known in application</returns>
        [HttpGet]
        [Route("profile")]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        public async Task<ActionResult<GetCurrentUserProfile.Response>> GetUserProfile()
        {
            var response = await _mediator.Send(new GetCurrentUserProfile.Query());
            return response.ToActionResult();
        }

        /// <summary>
        ///     Creates user from Azure AD profile. (name, surname and email (UPN))
        /// </summary>
        /// <returns>Notification if user creation was successful. ProblemDetail class (.NET Core) in case of error.</returns>
        [HttpPost]
        [Route("initiate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AutoCreateUserAfterLogin.Result>> CreateUserFromIdentity()
        {
            Log.Debug("CreateUserFromIdentity called by user: {Identity}", HttpContext.User.Identity.Name);
            // user be authenticated previously with Azure AD token, but this action is AllowAnonymous because user is not yet created and approved in our db
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            Log.Debug("User {Identity} is authorized", HttpContext.User.Identity.Name);

            var identity = HttpContext.User.Identities.FirstOrDefault();
            var result = await _mediator.Send(identity.ToCommand());

            await _unitOfWork.SaveChangesAsync();

            Log.Information("CreateUserFromIdentity finished with valid user: {0}", result.Created);

            return result;
        }

        /// <summary>
        ///     Logout user from the application with his Azure AD account
        /// </summary>
        /// <returns>User email when logout is successful</returns>
        [HttpPost]
        [Route("log-out")]
        [IgnoreUserSessionValidation]
        [UserHasPermission(PolicyNames.BackOffice.AnyPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserSessionTerminate.Result>> LogOut()
        {
            var result = await _mediator
                .Send(UserSessionTerminate.Command.Create(AccessToken.CreateFromHeaders(HttpContext.Request.Headers)));
            await _unitOfWork.SaveChangesAsync();
            Log.Information($"User {result.UserEmail} logged out.");
            return result;
        }
    }
}
