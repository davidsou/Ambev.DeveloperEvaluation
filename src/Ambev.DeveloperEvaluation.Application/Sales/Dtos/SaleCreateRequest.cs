namespace Ambev.DeveloperEvaluation.Application.Sales.Dtos;

public record SaleCreateRequest(
    Guid CustomerId,
    Guid BranchId,
    List<SaleItemRequest> Items
);

public record SaleItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Discount
);