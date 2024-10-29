namespace Catalog.Infrastructure.Repository;

public class GenderRepository : GenericRepository<Gender, Guid>, IGenderRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public GenderRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
