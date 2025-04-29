namespace Ambev.DeveloperEvaluation.Application.Carts.Dtos;

public class CheckoutResult
{
    public Guid UserId { get; set; }
    public List<CheckoutItem> Items { get; set; } = [];
    public decimal TotalOriginalPrice => Items.Sum(i => i.OriginalPrice);
    public decimal TotalDiscount => Items.Sum(i => i.DiscountAmount);
    public decimal TotalFinalPrice => Items.Sum(i => i.FinalPrice);
}
