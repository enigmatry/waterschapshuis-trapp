using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net.Mime;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Security;
using Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Users;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Accounts
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v{version:apiVersion}/[controller]")]
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
        public async Task<ActionResult<GetCurrentUserProfile.Response>> GetUserProfile()
        {
            var response = await _mediator.Send(new GetCurrentUserProfile.Query());
            return response.ToActionResult();
        }

        /// <summary>
        ///     Creates a new session for the given user
        /// </summary>
        /// <returns>Indication if session creation was successful</returns>
        [HttpPost]
        [Route("user-session")]
        [IgnoreUserSessionValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserSessionCreate.Result> CreateUserSession()
        {
            return new UserSessionCreate.Result();
        }

        /// <summary>
        ///     Refreshes access token issued to a user
        /// </summary>
        /// <param name="command">The current value of the token</param>
        /// <returns>The new token value issued by authentication authority</returns>
        [HttpPost]
        [Route("access-token")] // on route change, revisit auth.interceptor
        [IgnoreUserSessionValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable IDE0060 // Remove unused parameter
        public ActionResult<SessionAccessTokenCreate.Result> RefreshToken(SessionAccessTokenCreate.Command command)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            return new SessionAccessTokenCreate.Result();
        }

        /// <summary>
        ///     Logout user from the application with his Azure AD account
        /// </summary>
        /// <returns>User email when logout is successful</returns>
        [HttpPost]
        [Route("log-out")]
        [IgnoreUserSessionValidation]
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
