using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

public interface ICartService
{
    Task<Cart?> GetCartAsync(Guid userId);
    Task CreateOrUpdateCartAsync(Cart cart);
    Task AddItemToCartAsync(Guid userId, CartItem item);
    Task RemoveItemFromCartAsync(Guid userId, Guid productId);
    Task DeleteCartAsync(Guid userId);
}
