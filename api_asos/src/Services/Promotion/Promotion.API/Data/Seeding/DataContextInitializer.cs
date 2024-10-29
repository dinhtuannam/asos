using Promotion.API.Data;
using Promotion.API.Data.Seeding;

namespace Identity.API.Data.Seeding;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}

	public async Task<int> InitDiscount()
	{
		return 0;
	}

	public async Task<int> InitDiscountType()
	{
		int rows = 0;
		if (!_context.DiscountTypes.Any())
		{
			var percentage = new DiscountType()
			{
				Id = DiscountTypeConstant.Percentage,
				Name = DiscountTypeConstant.Percentage,
				Description = ""
			};
			var money = new DiscountType()
			{
				Id = DiscountTypeConstant.Money,
				Name = DiscountTypeConstant.Money,
				Description = ""
			};
			var product = new DiscountType()
			{
				Id = DiscountTypeConstant.Product,
				Name = DiscountTypeConstant.Product,
				Description = ""
			};
			_context.DiscountTypes.Add(percentage);
			_context.DiscountTypes.Add(money);
			_context.DiscountTypes.Add(product);

			rows = await _context.SaveChangesAsync();
		}
		return rows;
	}

	public async Task SeedAsync()
	{
		try
		{
			await InitDiscountType();
			await InitDiscount();
		}
		catch(Exception ex) { }
	}
}
