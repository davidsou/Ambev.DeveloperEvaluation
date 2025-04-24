using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class ProductRating:BaseEntity
{
    public Guid ProductId { get; set; }

    // Propriedade de navegação
    public virtual Product Product { get; set; }

    public Guid UserId { get; set; }
    public double Rate { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
