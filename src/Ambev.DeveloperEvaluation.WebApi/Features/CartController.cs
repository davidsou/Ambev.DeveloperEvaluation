using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features;


[Route("api/[controller]")]
public class CartController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet()]
    public async Task<IActionResult> GetCart()
    {
        var result = await Mediator.Send(new GetCart.Query());
        return FromResult(result);
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItem([FromBody] AddItemToCartDto dto)
    {
        var result = await Mediator.Send(new AddItemToCart.Command( dto.Item));
        return FromResult(result);
    }

    [HttpDelete("remove-item")]
    public async Task<IActionResult> RemoveItem( [FromQuery] Guid productId)
    {
        var result = await Mediator.Send(new RemoveItemFromCart.Command( productId));
        return FromResult(result);
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteCart()
    {
        var result = await Mediator.Send(new DeleteCart.Command());
        return FromResult(result);
    }
}
