using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Catalog.Application.Features.ProductItemFeature.Commands;
using Catalog.Application.Features.ProductItemFeature.Queries;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductItemController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetAllQuery(request)));
        }

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
            return Ok(await Mediator.Send(new ProductItem_GetPaginationQuery(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new ProductItem_GetByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductItem request, IFormFile image)
        {
            return Ok(await Mediator.Send(new ProductItem_AddCommand(request, image)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid Id,ProductItem request, IFormFile image)
        {
            return Ok(await Mediator.Send(new ProductItem_UpdateCommand(Id,request, image)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            return Ok(await Mediator.Send(new ProductItem_DeleteCommand(request)));
        }
    }
}
