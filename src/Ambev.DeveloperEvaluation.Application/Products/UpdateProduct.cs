using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class UpdateProduct
{
    public record Command(Guid Id, CreateProductDto Product) : IRequest<OperationResult>;

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

                entity.Title = request.Product.Title;
                entity.Price = request.Product.Price;
                entity.Description = request.Product.Description;
                entity.Category = request.Product.Category;
                entity.Image = request.Product.Image;
                entity.ChangedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(entity);
                return OperationResult.Success("Product updated successfully.");
            }, "Update product");
        }
    }
}
