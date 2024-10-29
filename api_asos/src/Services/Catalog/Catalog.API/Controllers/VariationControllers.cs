using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Catalog.Application.Features.VariationFeature.Commands;
using Catalog.Application.Features.VariationFeature.Queries;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationControllers : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetAllQuery(request)));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new Variation_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new Variation_GetByIdQuery(id)));
        }


        [HttpPost]
        public async Task<IActionResult> Create(Variation request)
        {
            return Ok(await Mediator.Send(new Variation_AddCommand(request)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Variation request)
        {
            return Ok(await Mediator.Send(new Variation_UpdateCommand(request)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new Variation_DeleteCommand(request)));
        }
    }
}
