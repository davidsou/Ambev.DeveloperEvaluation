using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Users.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class DeleteCart
{
    public record Command : IRequest<OperationResult>;

    public class Handler(ICartService cartService,
        ICurrentUserService currentUser,
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {
       
        public async Task<OperationResult> Handle(Command _, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {

                if (!currentUser.IsAuthenticated)
                {
                    return OperationResult.Failure("User not Authenticated or not found");
                }

                var userId = new Guid(currentUser.UserId!);

                await cartService.DeleteCartAsync(userId);
                return OperationResult.Success("Cart deleted successfully.");
            }, "Delete cart");
        }
    }
}

