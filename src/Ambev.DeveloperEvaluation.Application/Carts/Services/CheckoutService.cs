using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts.Services;

public class CheckoutService(ICartService cartService) : ICheckoutService
{


    public async Task<OperationResult<CheckoutResult>> CheckoutCartAsync(Guid userId)
    {
        var cart = await cartService.GetCartAsync(userId);
        if (cart == null || cart.CartItems.Count<=0)
            return OperationResult<CheckoutResult>.Failure("Cart is empty or not found.");

        var result = new CheckoutResult { UserId = userId };

        foreach (var item in cart.CartItems)
        {
            if (item.Quantity > 20)
            {
                return OperationResult<CheckoutResult>.Failure($"Product '{item.ProductName}' exceeds the maximum allowed quantity (20 units).");
            }

            var discountPercent = GetDiscountPercent(item.Quantity);

            result.Items.Add(new CheckoutItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                DiscountPercent = discountPercent
            });
        }

        return OperationResult<CheckoutResult>.Success(result);
    }

    private static decimal GetDiscountPercent(int quantity)
    {
        if (quantity >= 10 && quantity <= 20) return 20m;
        if (quantity >= 4 && quantity < 10) return 10m;
        return 0m;
    }
}

