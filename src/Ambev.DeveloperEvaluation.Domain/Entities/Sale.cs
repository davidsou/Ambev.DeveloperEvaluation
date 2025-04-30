using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public virtual User Customer { get; set; }
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; } = false;
    public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}
