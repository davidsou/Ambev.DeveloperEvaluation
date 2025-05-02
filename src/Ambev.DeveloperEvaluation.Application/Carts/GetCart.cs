using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Users.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class GetCart
{
    public record Query() : IRequest<OperationResult<Cart>>;

    public class Handler(ICartService cartService,
        ICurrentUserService currentUser, 
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Query, OperationResult<Cart>>
    {

        public async Task<OperationResult<Cart>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {

                if (!currentUser.IsAuthenticated)
                {
                    return OperationResult<Cart>.Failure("User not Authenticated or not found");
                }

                var userId = new Guid(currentUser.UserId!);


                var cart = await cartService.GetCartAsync(userId);
                if (cart == null)
                    return OperationResult<Cart>.Failure("Cart not found.");

                return OperationResult<Cart>.Success(cart);
            }, "Get cart");
        }
    }
}


