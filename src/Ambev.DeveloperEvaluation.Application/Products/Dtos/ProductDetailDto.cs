namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record ProductDetailDto(
    int Id,
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image,
    double AverageRating,
    int TotalRatings,
    List<ProductRatingDto> Ratings
);
