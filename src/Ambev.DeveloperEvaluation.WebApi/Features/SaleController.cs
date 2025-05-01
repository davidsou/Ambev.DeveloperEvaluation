using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleCreateRequest request)
        {
            var result = await Mediator.Send(new CreateSale.Command(request));
            return FromResult(result);
        }
    }
}
