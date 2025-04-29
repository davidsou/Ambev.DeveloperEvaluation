using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;
public class CartService : ICartService
{
    private readonly ICacheService _cacheService;
    private const string CartPrefix = "cart:";

    public CartService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    private static string GetCartKey(Guid userId) => $"{CartPrefix}{userId}";

    public async Task<Cart?> GetCartAsync(Guid userId)
    {
        return await _cacheService.GetAsync<Cart>(GetCartKey(userId));
    }

    public async Task CreateOrUpdateCartAsync(Cart cart)
    {
        // Não precisa mais recalcular o TotalPrice manualmente
        await _cacheService.SetAsync(GetCartKey(cart.UserId), cart, TimeSpan.FromHours(2));
    }

    public async Task AddItemToCartAsync(Guid userId, CartItem item)
    {
        var cart = await GetCartAsync(userId) ?? new Cart { UserId = userId };
        cart.AddOrUpdateItem(item);

        await CreateOrUpdateCartAsync(cart);
    }

    public async Task RemoveItemFromCartAsync(Guid userId, Guid productId)
    {
        var cart = await GetCartAsync(userId);
        if (cart == null) return;

        cart.RemoveItem(productId);
        await CreateOrUpdateCartAsync(cart);
    }

    public async Task DeleteCartAsync(Guid userId)
    {
        await _cacheService.RemoveAsync(GetCartKey(userId));
    }
}