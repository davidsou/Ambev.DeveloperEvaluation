using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class Product:BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    // Propriedade de navegação
    public virtual ICollection<ProductRating> Ratings { get; set; }

    // Propriedades agregadas
    public double AverageRating => Ratings?.Count > 0 ? Ratings.Average(r => r.Rate) : 0.0;
    public int TotalRatings => Ratings?.Count ?? 0;
}
