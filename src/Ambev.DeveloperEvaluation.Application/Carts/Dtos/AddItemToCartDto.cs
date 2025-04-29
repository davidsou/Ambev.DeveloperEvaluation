namespace Ambev.DeveloperEvaluation.Application.Carts.Dtos;

public class AddItemToCartDto
{
    public Guid UserId { get; set; }
    public CartItemDto Item { get; set; } = new();
}