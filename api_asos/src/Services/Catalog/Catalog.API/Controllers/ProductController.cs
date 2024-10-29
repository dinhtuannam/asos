using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Catalog.Application.Features.ProductFeature.Commands;
using Catalog.Application.Features.ProductFeature.Queries;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace User.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : BaseController
	{

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
            return Ok(await Mediator.Send(new Product_GetAllQuery(request)));
        }

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetFilterQuery(request)));
		}

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
			return Ok(await Mediator.Send(new Product_GetPaginationQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
            return Ok(await Mediator.Send(new Product_GetByIdQuery(id)));
        }

		[HttpGet("slug/{slug}")]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok(await Mediator.Send(new Product_GetBySlugQuery(slug)));
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product request)
		{
			return Ok(await Mediator.Send( new Product_AddCommand( request)));
		}

		[HttpPut]
		public async Task<IActionResult> Update(Product request)
		{
			return Ok(await Mediator.Send(new Product_UpdateCommand(request)));
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteRequest request)
		{
			return Ok(await Mediator.Send(new Product_DeleteCommand(request)));
		}
	}
}
