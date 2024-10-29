using BuildingBlock.Core.Request;
using BuildingBlock.Core.WebApi;
using Catalog.Application.Features.ColorFeature.Commands;
using Catalog.Application.Features.ColorFeature.Queries;
using Catalog.Application.Features.GenderFeature.Commands;
using Catalog.Application.Features.GenderFeature.Queries;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ColorController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new Color_GetAllQuery(request)));
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetFilter([FromQuery] FilterRequest request)
		{
            return Ok(await Mediator.Send(new Color_GetFilterQuery(request)));
        }

		[HttpGet("pagination")]
		public async Task<IActionResult> GetPagination([FromQuery] PaginationRequest request)
		{
            return Ok(await Mediator.Send(new Color_GetPaginationQuery(request)));
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			return Ok(await Mediator.Send(new Color_GetByIdQuery(id)));
		}

		[HttpGet("slug/{slug}")]
		public async Task<IActionResult> GetBySlug([FromRoute] string slug)
		{
			return Ok( await Mediator.Send(new Color_GetBySlugQuery (slug)));
		}

		[HttpPost]
		public async Task<IActionResult> Create(Color request)
		{
            return Ok(await Mediator.Send(new Color_AddCommand(request)));
        }

		[HttpPut]
		public async Task<IActionResult> Update(Color request)
		{
            return Ok(await Mediator.Send(new Color_UpdateCommand(request)));
        }

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteRequest request)
		{
            return Ok(await Mediator.Send(new Color_DeleteCommand(request)));
        }
	}
}
