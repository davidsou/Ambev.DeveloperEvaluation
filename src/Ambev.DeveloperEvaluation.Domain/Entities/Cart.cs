using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart:BaseEntity
{
    public Guid UserId { get; set; }
    public List<CartItem> CartItems { get; set; }=new List<CartItem>();
    public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

    public void AddOrUpdateItem(CartItem newItem)
    {
        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == newItem.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
            existingItem.Price = newItem.Price; 
        }
        else
        {
            CartItems.Add(newItem);
        }
    }

    // Método auxiliar para remover um item
    public void RemoveItem(Guid productId)
    {
        CartItems.RemoveAll(i => i.ProductId == productId);
    }

}

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } =string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}

