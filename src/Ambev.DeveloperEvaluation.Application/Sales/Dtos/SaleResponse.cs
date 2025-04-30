namespace Ambev.DeveloperEvaluation.Application.Sales.Dtos;

public record SaleResponse(
    Guid SaleId,
    string SaleNumber,
    DateTime SaleDate,
    string CustomerName,
    string BranchName,
    decimal TotalAmount,
    bool IsCancelled,
    List<SaleItemResponse> Items
);

public record SaleItemResponse(
    Guid ProductId,
    string ProductTitle,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal Total
);
