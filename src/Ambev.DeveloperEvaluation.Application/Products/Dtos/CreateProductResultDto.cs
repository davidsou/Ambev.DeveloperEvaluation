namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record CreateProductResultDto(
    int Id,
    string Title,
    decimal Price,
    string Category,
    double AverageRating,
    int TotalRatings
);
