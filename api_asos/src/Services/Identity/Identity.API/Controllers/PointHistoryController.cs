using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Identity.API.Features.PointHistoryFeature.Queries;
using Identity.API.Features.PointHistoryFeature.Commands;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PointHistoryController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] BaseRequest request)
		{
			return Ok(await Mediator.Send(new PointHistory_GetAllQuery(request)));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			return Ok(await Mediator.Send(new PointHistory_GetByIdQuery(id)));
		}

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterRequest request)
        {
            return Ok(await Mediator.Send(new PointHistory_GetFilterQuery(request)));
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
        {
            return Ok(await Mediator.Send(new PointHistory_GetPaginationQuery(request)));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            return Ok(await Mediator.Send(new PointHistory_DeleteCommand(request)));
        }

    }
}
