namespace Catalog.Infrastructure.Repository;

public class BrandRepository : GenericRepository<Brand, Guid>, IBrandRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public BrandRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
