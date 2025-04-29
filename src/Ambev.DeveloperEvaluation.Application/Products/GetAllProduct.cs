using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class GetAllProducts
{
    public record Query() : IRequest<OperationResult<List<ProductDto>>>;

    public class Handler(IProductRepository repository,
         IMapper mapper, 
         ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Query, OperationResult<List<ProductDto>>>
    {
        private readonly IProductRepository _repository = repository;

        public async Task<OperationResult<List<ProductDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var list = await _repository.GetAllWithRatingsAsync();
                var dtos = mapper.Map<List<ProductDto>>(list);
                return OperationResult<List<ProductDto>>.Success(dtos);
            }, "List products");
        }
    }
}
