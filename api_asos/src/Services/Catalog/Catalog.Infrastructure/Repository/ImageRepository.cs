namespace Catalog.Infrastructure.Repository;

public class ImageRepository : GenericRepository<Image, Guid>, IImageRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ImageRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
    public async Task<List<Image>> GetImagesByProductItemIdAsync(Guid productItemId)
    {
        return await _dbSet.Where(img => img.ProductItemId == productItemId).ToListAsync();
    }
}
