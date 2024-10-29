namespace Catalog.Infrastructure.Repository;

public class ProductItemRepository : GenericRepository<ProductItem, Guid>, IProductItemRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ProductItemRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
