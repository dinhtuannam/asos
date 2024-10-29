using BuildingBlock.Core.WebApi;
using BuildingBlock.Grpc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : BaseController
	{
		private readonly IdentityGrpcService _identityGrpcService;

		public OrderController(IdentityGrpcService identityGrpcService)
		{
			_identityGrpcService = identityGrpcService;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUserInfo(Guid userId)
		{
			var user = await _identityGrpcService.GetUserAsync(userId);
			return Ok(user);
		}
	}
}
