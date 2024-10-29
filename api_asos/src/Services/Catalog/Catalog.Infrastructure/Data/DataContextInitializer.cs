namespace Catalog.Infrastructure.Data;

public class DataContextInitializer : IDataContextInitializer
{
	private readonly DataContext _context;
	public DataContextInitializer(DataContext context)
	{
		_context = context;
	}

    public async Task  InitBrand()
    {
        if (!_context.Brands.Any())
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Description = "Loai Pho Biên",
                    Name = "Dell",
                    Slug = "https://sharecode.vn/",
                    Image="D://ABC",
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    Description = "Red",
                    Name = "Red",
                    Slug = "red",
                    Image="D://ABC",
                    CreatedUser = Guid.NewGuid(),
                    CreatedDate=DateTime.UtcNow,
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                }
            };

            _context.Brands.AddRange(brands);
            await _context.SaveChangesAsync();
        }
    }

    public async Task  InitCategory()
    {
        if (!_context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Description = "Loai Pho Biên",
                    Name = "Dell",
                    Slug = "https://sharecode.vn/",
   
                   CreatedDate=DateTime.UtcNow,
                   ImageFile="D://ABC",
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                },
                new Category
                {
                     Id = Guid.NewGuid(),
                     Description = "Loai Pho Biên",
                     Name = "Acer",
                     Slug = "https://sharecode.vn/",
                   
                     CreatedDate=DateTime.UtcNow,
                     ImageFile="D://ABC",
                   
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                }
            };

            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitColor()
    {
        if (!_context.Colors.Any())
        {
           
            var colors = new List<Color>
            {
                new Color
                {
                    Id = Guid.NewGuid(),
                    Description = "Yellow",
                    Name = "Yellow",
                    Slug = "yellow",
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                },
                new Color
                {
                    Id = Guid.NewGuid(),
                    Description = "Red",
                    Name = "Red",
                    Slug = "red",
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                }
            };

            _context.Colors.AddRange(colors);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitProduct()
	{
        if (!_context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description="Hang Tot",
                    Name = "LapTop Dell Inspiration ",
                    AverageRating=5,
                    SizeAndFit="XXL",
                    Slug = "https://www.facebook.com/groups/thuedoancntt",
                    CreatedUser = Guid.NewGuid(),
                 
                    CreatedDate=DateTime.UtcNow,
                    
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description="Hang Tot",
                    Name = "LapTop Gaming ",
                    AverageRating=10,
                    SizeAndFit="XXXL",
                   
                    Slug = "https://www.facebook.com/groups/thuedoancntt",
                    CreatedDate=DateTime.UtcNow,
                    CreatedUser = Guid.NewGuid(),
                    DeleteFlag = false,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedUser = Guid.NewGuid()
                }
            };

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }

	public async Task SeedAsync()
	{
        try
        {
            await InitBrand();
            await InitCategory();
            await InitColor();
            await InitProduct();
        }catch(Exception ex)
        {
            Console.WriteLine($"Error seeding data: {ex.Message}");
        }
	}
}
