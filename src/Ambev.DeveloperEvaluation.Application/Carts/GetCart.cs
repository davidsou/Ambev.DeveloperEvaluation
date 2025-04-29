using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class GetCart
{
    public record Query(Guid UserId) : IRequest<OperationResult<Cart>>;

    public class Handler(ICartService cartService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Query, OperationResult<Cart>>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<OperationResult<Cart>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var cart = await _cartService.GetCartAsync(request.UserId);
                if (cart == null)
                    return OperationResult<Cart>.Failure("Cart not found.");

                return OperationResult<Cart>.Success(cart);
            }, "Get cart");
        }
    }
}


