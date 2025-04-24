using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class DeleteProduct
{
    public record Command(int Id) : IRequest<OperationResult>;

    public class Handler(IProductRepository repository, ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity == null)
                    return OperationResult.Failure("Product not found.");

                await _repository.DeleteAsync(entity);
                return OperationResult.Success("Product deleted successfully.");
            }, "Delete product");
        }
    }
}
