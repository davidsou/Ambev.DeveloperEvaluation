using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Users.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class RemoveItemFromCart
{
    public record Command( Guid ProductId) : IRequest<OperationResult>;

    public class Handler(ICartService cartService,
        ICurrentUserService currentUser, 
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {

        public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                if (!currentUser.IsAuthenticated)
                {
                    return OperationResult.Failure("User not Authenticated or not found");
                }

                var userId = new Guid(currentUser.UserId!);

                await cartService.RemoveItemFromCartAsync(userId, request.ProductId);
                return OperationResult.Success("Item removed from cart.");
            }, "Remove item from cart");
        }
    }
}

