namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public record CreateProductDto(
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image
);
