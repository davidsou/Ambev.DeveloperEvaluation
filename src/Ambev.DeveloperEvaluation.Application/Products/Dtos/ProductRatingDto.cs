namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record ProductRatingDto(
    int Id,
    int Rate,
    string Comment,
    DateTime CreatedAt
);
