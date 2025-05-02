namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record ProductRatingDto(
    Guid Id,
    int Rate,
    string Comment,
    DateTime CreatedAt
);
