namespace Catalog.Infrastructure.Repository;

public class SizeCategoryRepository : GenericRepository<SizeCategory, Guid>, ISizeCategoryRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public SizeCategoryRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
