namespace Catalog.Infrastructure.Repository;

public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public CommentRepository(DataContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
	{
		_unitOfWork = unitOfWork;
		_context = context;
		_mapper = mapper;
	}
}
