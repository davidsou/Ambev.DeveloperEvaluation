using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class AddItemToCart
{
    public record Command(Guid UserId, CartItemDto Item) : IRequest<OperationResult>;

    public class Handler(ICartService cartService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {

        public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var cartItem = new CartItem
                {
                    ProductId = request.Item.ProductId,
                    ProductName = request.Item.ProductName,
                    Quantity = request.Item.Quantity,
                    Price = request.Item.Price
                };

                await cartService.AddItemToCartAsync(request.UserId, cartItem);
                return OperationResult.Success("Item added to cart.");
            }, "Add item to cart");
        }
    }
}
