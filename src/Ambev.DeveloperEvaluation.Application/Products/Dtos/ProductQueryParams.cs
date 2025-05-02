using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Products.Dtos;

public class ProductQueryParams:QueryParamsBase
{
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public string? Title { get; set; }    
    public string? Description{ get; set; }
    public string? Category{ get; set; }
}
