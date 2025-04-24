using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class CreateProduct
{
    public record Command(CreateProductDto Product) : IRequest<OperationResult<Guid>>;

    public class Handler(IProductRepository repository,
          IMapper mapper, 
          ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Command, OperationResult<Guid>>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<OperationResult<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var entity = mapper.Map<Product>(request.Product);
                await _repository.AddAsync(entity);
                return OperationResult<Guid>.Success(entity.Id, "Product created successfully.");
            }, "Create product");
        }
    }
}
