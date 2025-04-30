using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services;

public interface ISaleService
{
    Task<OperationResult<SaleResponse>> CreateSaleAsync(SaleCreateRequest request);
    Task<OperationResult<IEnumerable<SaleResponse>>> GetAllSalesAsync();
    Task<OperationResult<SaleResponse>> GetSaleByIdAsync(Guid id);
    Task<OperationResult> CancelSaleAsync(Guid id);
}
