namespace Ambev.DeveloperEvaluation.Application.Carts.Dtos;

public class AddItemToCartDto
{
    public CartItemDto Item { get; set; } = new();
}