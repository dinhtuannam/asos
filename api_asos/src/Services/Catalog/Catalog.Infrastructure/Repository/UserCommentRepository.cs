namespace Catalog.Infrastructure.Repository;

public class UserCommentRepository : GenericRepository<UserComment, Guid>, IUserCommentRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public UserCommentRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
