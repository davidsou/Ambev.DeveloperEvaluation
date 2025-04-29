using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Application.Carts.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CheckoutCart
{
    public record Command(Guid UserId) : IRequest<OperationResult<CheckoutResult>>;

    public class Handler(ICheckoutService checkoutService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult<CheckoutResult>>
    {
        public async Task<OperationResult<CheckoutResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var result = await checkoutService.CheckoutCartAsync(request.UserId);
                return result;
            }, "Checkout cart");
        }
    }
}

