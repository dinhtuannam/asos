namespace Ordering.API.Constants;

public static class OrderStatusConstant
{
	public static string Pending = nameof(Pending);
	public static string Placed = nameof(Placed);
	public static string Packed = nameof(Packed);
	public static string Shipping = nameof(Shipping);
	public static string Completed = nameof(Completed);
	public static string Refunded = nameof(Refunded);
}
