using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features;

[Route("api/[controller]")]
public class ProductController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var result = await Mediator.Send(new CreateProduct.Command(dto));
        return FromResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
    {
        var result = await Mediator.Send(new UpdateProduct.Command(id, dto));
        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteProduct.Command(id));
        return FromResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetProductById.Query(id));
        return FromResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllProducts.Query());
        return FromResult(result);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] ProductQueryParams queryParams)
    {
        var result = await Mediator.Send(new QueryProducts.Query(queryParams));
        return FromResult(result);
    }

    [HttpGet("detailed/{id}")]
    public async Task<IActionResult> GetDetailedById(Guid id)
    {
        var result = await Mediator.Send(new QueryProductById.Query(id));
        return FromResult(result);
    }
}

