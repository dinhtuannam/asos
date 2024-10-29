namespace Catalog.Infrastructure.Repository;

public class WishlistRepository : GenericRepository<WishList, Guid>, IWishlistRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public WishlistRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}