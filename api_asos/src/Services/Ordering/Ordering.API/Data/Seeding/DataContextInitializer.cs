namespace Ordering.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}
	public async Task<int> InitOrderStatus()
	{
		int rows = 0;
		if (!_context.OrderStatus.Any())
		{
			var statuses = new List<OrderStatus>()
			{
				new OrderStatus()
				{
					Id = OrderStatusConstant.Pending,
					Name = "Pending Payment",
					Description = "Chờ thanh toán"
				},
				new OrderStatus()
				{
					Id = OrderStatusConstant.Placed,
					Name = "Order Placed",
					Description = "Đã đặt hàng"
				},
				new OrderStatus()
				{
					Id = OrderStatusConstant.Packed,
					Name = "Order Packaged",
					Description = "Đã đóng gói"
				},
				new OrderStatus()
				{
					Id = OrderStatusConstant.Shipping,
					Name = "Shipping",
					Description = "Đang giao"
				},
				new OrderStatus()
				{
					Id = OrderStatusConstant.Completed,
					Name = "Completed",
					Description = "Hoàn thành"
				},
				new OrderStatus()
				{
					Id = OrderStatusConstant.Refunded,
					Name = "Refunded",
					Description = "Đã hoàn tiền"
				}
			};
			_context.OrderStatus.AddRange(statuses);
			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitOrderStatus();
		}
		catch(Exception ex) { }
	}
}
