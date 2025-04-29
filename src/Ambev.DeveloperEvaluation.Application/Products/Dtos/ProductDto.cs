namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record ProductDto(
    Guid Id,
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image,
    double AverageRating,
    int TotalRatings
);
