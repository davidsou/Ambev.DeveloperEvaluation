using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class DeleteCart
{
    public record Command(Guid UserId) : IRequest<OperationResult>;

    public class Handler(ICartService cartService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {
        private readonly ICartService _cartService = cartService;

        public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                await _cartService.DeleteCartAsync(request.UserId);
                return OperationResult.Success("Cart deleted successfully.");
            }, "Delete cart");
        }
    }
}

