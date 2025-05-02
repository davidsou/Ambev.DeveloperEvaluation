using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Application.Users.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class AddItemToCart
{
    public record Command( CartItemDto Item) : IRequest<OperationResult>;

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

                var cartItem = new CartItem
                {
                    ProductId = request.Item.ProductId,
                    ProductName = request.Item.ProductName,
                    Quantity = request.Item.Quantity,
                    Price = request.Item.Price
                };

                await cartService.AddItemToCartAsync(userId, cartItem);
                return OperationResult.Success("Item added to cart.");
            }, "Add item to cart");
        }
    }
}
