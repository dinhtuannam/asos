namespace Catalog.Infrastructure.Repository;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public CategoryRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
