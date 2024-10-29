using Catalog.Infrastructure.Repository;

namespace Catalog.Infrastructure.Seedworks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public IGenderRepository Genders { get; private set; }
	public IColorRepository Colors { get; private set; }
	public IBrandRepository Brands { get; private set; }
	public ICategoryRepository Categories { get; private set; }
	public ICommentRepository Comments { get; private set; }
	public IImageRepository Images { get; private set; }
	public IProductItemRepository ProductItems { get; private set; }
	public IProductRepository Products { get; private set; }
	public IRatingRepository Ratings { get; private set; }
	public ISizeCategoryRepository SizeCategories { get; private set; }
	public ISizeRepository Sizes { get; private set; }
	public IUserCommentRepository UserComments { get; private set; }
	public IVariationRepository Variations { get; private set; }
	public IWishlistRepository Wishlists { get; private set; }

	public UnitOfWork(
		DataContext context,
		IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
		Genders = new GenderRepository(_context, _mapper, this);
		Colors = new ColorRepository(_context, _mapper, this);
		Brands = new BrandRepository(_context, _mapper, this);
		Categories = new CategoryRepository(_context, _mapper, this);
		Comments = new CommentRepository(_context, _mapper, this);
		Images = new ImageRepository(_context, _mapper, this);
		ProductItems = new ProductItemRepository(_context, _mapper, this);
		Products = new ProductRepository(_context, _mapper, this);
		Ratings = new RatingRepository(_context, _mapper, this);
		SizeCategories = new SizeCategoryRepository(_context, _mapper, this);
		Sizes = new SizeRepository(_context, _mapper, this);
		UserComments = new UserCommentRepository(_context, _mapper, this);
		Variations = new VariationRepository(_context, _mapper, this);
		Wishlists = new WishlistRepository(_context, _mapper, this);
	}

	public async Task<int> CompleteAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}