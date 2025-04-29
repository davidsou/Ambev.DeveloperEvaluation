using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

public interface ICheckoutService
{
    Task<OperationResult<CheckoutResult>> CheckoutCartAsync(Guid userId);
}
