using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class QueryProductById
{
    public record Query(Guid Id, Expression<Func<Product, object>>[]? Includes = null) : IRequest<OperationResult<ProductDto>>;

    public class Handler(IProductRepository repository,
        IMapper mapper,
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Query, OperationResult<ProductDto>>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<OperationResult<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var includes = request.Includes;

                var items = await _repository.FindWithIncludesAsync(p => p.Id == request.Id, includes);
                var entity = items.FirstOrDefault();

                if (entity == null)
                    return OperationResult<ProductDto>.Failure("Product not found.");

                var dto = mapper.Map<ProductDto>(entity);
                return OperationResult<ProductDto>.Success(dto);
            }, "Detailed product query");
        }
    }
}
