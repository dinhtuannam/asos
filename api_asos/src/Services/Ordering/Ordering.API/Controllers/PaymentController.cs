using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Interfaces;

namespace Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : BaseController
	{
		private readonly IPaymentService _paymentService;
		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpPost("vnpay")]
		public async Task<IActionResult> Payment()
		{
			string user = HttpContext.Items["code"] as string ?? "";
			string url = await _paymentService.CreatePaymentUrl(user, HttpContext);
			return Ok(url);
		}
	}
}
