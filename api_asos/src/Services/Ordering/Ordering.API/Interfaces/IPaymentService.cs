namespace Ordering.API.Interfaces;

public interface IPaymentService
{
	Task<string> CreatePaymentUrl(string user, HttpContext context);
	PaymentResponseModel PaymentExecute(IQueryCollection collections);
}
