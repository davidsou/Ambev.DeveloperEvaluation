using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class GetProductById
{
    public record Query(Guid Id) : IRequest<OperationResult<ProductDto>>;

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
                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity == null)
                    return OperationResult<ProductDto>.Failure("Product not found.");

                var dto = mapper.Map<ProductDto>(entity);
                return OperationResult<ProductDto>.Success(dto);
            }, "Get product by ID");
        }
    }
}
