using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products;
public class QueryProducts
{
    public record Query(ProductQueryParams Params) : IRequest<OperationResult<PagedResult<ProductDetailDto>>>;

    public class Handler(
        IProductRepository repository,
        IMapper mapper,
        ILogger<Handler> logger)
        : BaseHandler(logger), IRequestHandler<Query, OperationResult<PagedResult<ProductDetailDto>>>
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationResult<PagedResult<ProductDetailDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await TryCatchAsync(async () =>
            {
                var parameters = request.Params;

                // Aplicando filtros mais detalhados
                var queryOptions = new QueryOptions<Product>
                {
                    Filter = p =>
                        (string.IsNullOrEmpty(parameters.Category) || EF.Functions.Like(p.Category, parameters.Category.Replace("*", "%"))) &&
                        (string.IsNullOrEmpty(parameters.Title) || EF.Functions.Like(p.Title, parameters.Title.Replace("*", "%"))) &&
                        (string.IsNullOrEmpty(parameters.Description) || EF.Functions.Like(p.Description, parameters.Description.Replace("*", "%"))) &&
                        (!parameters.MinPrice.HasValue || p.Price >= parameters.MinPrice.Value) &&
                        (!parameters.MaxPrice.HasValue || p.Price <= parameters.MaxPrice.Value),
                    OrderBy = parameters.OrderBy,
                    OrderDescending = parameters.OrderDescending,
                    Skip = parameters.Skip,
                    Take = parameters.Take,
                    AsNoTracking = true
                };

                var totalItems = await _repository.CountAsync(queryOptions.Filter);
                var items = await _repository.QueryAsync(queryOptions);

                var pageSize = parameters.Take ?? totalItems;
                var currentPage = (parameters.Skip.HasValue && pageSize > 0)
                    ? (parameters.Skip.Value / pageSize) + 1
                    : 1;

                var totalPages = pageSize > 0 ? (int)Math.Ceiling(totalItems / (double)pageSize) : 1;

                var dtoItems = _mapper.Map<List<ProductDetailDto>>(items);

                return OperationResult<PagedResult<ProductDetailDto>>.Success(new PagedResult<ProductDetailDto>
                {
                    TotalCount = totalItems,
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    TotalPages = totalPages,
                    Items = dtoItems
                });
            }, "Paged product query");
        }
    }
}