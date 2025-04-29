using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class RemoveItemFromCart
{
    public record Command(Guid UserId, Guid ProductId) : IRequest<OperationResult>;

    public class Handler(ICartService cartService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                await _cartService.RemoveItemFromCartAsync(request.UserId, request.ProductId);
                return OperationResult.Success("Item removed from cart.");
            }, "Remove item from cart");
        }
    }
}

