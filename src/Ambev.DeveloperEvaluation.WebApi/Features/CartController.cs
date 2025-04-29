using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features;


[Route("api/[controller]")]
public class CartController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(Guid userId)
    {
        var result = await Mediator.Send(new GetCart.Query(userId));
        return FromResult(result);
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItem([FromBody] AddItemToCartDto dto)
    {
        var result = await Mediator.Send(new AddItemToCart.Command(dto.UserId, dto.Item));
        return FromResult(result);
    }

    [HttpDelete("remove-item")]
    public async Task<IActionResult> RemoveItem([FromQuery] Guid userId, [FromQuery] Guid productId)
    {
        var result = await Mediator.Send(new RemoveItemFromCart.Command(userId, productId));
        return FromResult(result);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteCart(Guid userId)
    {
        var result = await Mediator.Send(new DeleteCart.Command(userId));
        return FromResult(result);
    }
}
