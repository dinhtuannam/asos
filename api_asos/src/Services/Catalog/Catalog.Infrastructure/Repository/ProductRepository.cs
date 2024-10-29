namespace Catalog.Infrastructure.Repository;

public class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ProductRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
