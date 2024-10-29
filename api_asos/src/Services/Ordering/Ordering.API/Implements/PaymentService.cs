using Microsoft.Extensions.Options;
using Ordering.API.Extensions;
using Ordering.API.Interfaces;
using Ordering.API.Settings;

namespace Ordering.API.Implements;

public class PaymentService : IPaymentService
{
	private readonly DataContext _context;
	private readonly VnpaySettings _settings;

	public PaymentService(DataContext context, IOptions<VnpaySettings> settings)
	{
		_settings = settings.Value;
		_context = context;
	}

	public async Task<string> CreatePaymentUrl(string user, HttpContext context)
	{
		try
		{
			var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_settings.TimeZoneId);
			var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
			var tick = DateTime.Now.Ticks.ToString();
			var pay = new VnPayLibrary();
			var urlCallBack = _settings.ReturnUrl;
			//CartItem cart = await _unitOfWork.cart.getCartDetail(user);

			pay.AddRequestData("vnp_Version", _settings.Version);
			pay.AddRequestData("vnp_Command", _settings.Command);
			pay.AddRequestData("vnp_TmnCode", _settings.TmnCode);
			pay.AddRequestData("vnp_Amount", ((int)50000 * 100).ToString());
			pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
			pay.AddRequestData("vnp_CurrCode", _settings.CurrCode);
			pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
			pay.AddRequestData("vnp_Locale", _settings.Locale);
			pay.AddRequestData("vnp_OrderInfo", $"PAYMENT_CODE");
			pay.AddRequestData("vnp_OrderType", "course");
			pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
			pay.AddRequestData("vnp_TxnRef", tick);

			var paymentUrl = pay.CreateRequestUrl(_settings.BaseUrl, _settings.HashSecret);

			return paymentUrl;
		}
		catch (Exception ex)
		{

			return "";
		}
	}

	public PaymentResponseModel PaymentExecute(IQueryCollection collections)
	{
		var pay = new VnPayLibrary();
		var response = pay.GetFullResponseData(collections, _settings.HashSecret);

		return response;
	}
}