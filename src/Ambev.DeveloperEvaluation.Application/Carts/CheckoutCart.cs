using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Users.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CheckoutCart
{
    public record Command : IRequest<OperationResult<CheckoutResult>>;

    public class Handler(ICheckoutService checkoutService,
        ICurrentUserService currentUser,
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult<CheckoutResult>>
    {
        public async Task<OperationResult<CheckoutResult>> Handle(Command _, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                if (!currentUser.IsAuthenticated)
                {
                    return OperationResult<CheckoutResult>.Failure("User not Authenticated or not found");
                }

                var userId = new Guid(currentUser.UserId!);

                var result = await checkoutService.CheckoutCartAsync(userId);
                return result;
            }, "Checkout cart");
        }
    }
}

