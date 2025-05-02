using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet()]
    public async Task<IActionResult> Checkout()
    {
        var result = await Mediator.Send(new CheckoutCart.Command());
        return FromResult(result);
    }
}
