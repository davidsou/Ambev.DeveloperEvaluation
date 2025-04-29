namespace Ambev.DeveloperEvaluation.Application.Carts.Dtos;

public class CheckoutItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OriginalPrice => Quantity * UnitPrice;
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount => OriginalPrice * (DiscountPercent / 100);
    public decimal FinalPrice => OriginalPrice - DiscountAmount;
}
