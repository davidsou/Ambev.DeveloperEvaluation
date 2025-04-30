using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.Services;
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class CreateSale
{
    public record Command(SaleCreateRequest Request) : IRequest<OperationResult<SaleResponse>>;

    public class Handler(ISaleService saleService, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult<SaleResponse>>
    {
   
        public async Task<OperationResult<SaleResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var result = await saleService.CreateSaleAsync(request.Request);
                return result;
            }, "Create Sale");
        }
    }
}