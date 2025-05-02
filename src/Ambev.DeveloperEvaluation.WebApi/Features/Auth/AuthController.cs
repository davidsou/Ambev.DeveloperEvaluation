using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth;

/// <summary>
/// Controller for authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController(IMediator mediator, IMapper mapper ) : BaseController(mediator)
{



    /// <summary>
    /// Authenticates a user with their credentials
    /// </summary>
    /// <param name="request">The authentication request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication token if successful</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new AuthenticateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = mapper.Map<AuthenticateUserCommand>(request);
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<AuthenticateUserResponse>
        {
            Success = true,
            Message = "User authenticated successfully",
            Data = mapper.Map<AuthenticateUserResponse>(response)
        });
    }
}
