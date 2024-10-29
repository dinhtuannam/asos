namespace Identity.API.Features.UserFeature.Commands;

public record User_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class User_DeleteCommandHandler : ICommandHandler<User_DeleteCommand, Result<bool>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public User_DeleteCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<bool>> Handle(User_DeleteCommand request, CancellationToken cancellationToken)
	{
		if (request.RequestData.Ids == null)
			throw new ApplicationException("Ids not found");

		List<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
		var query = await _context.Users.Where(m => ids.Contains(m.Id)).ToListAsync();
		if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

		foreach (var item in query)
		{
			item.DeleteFlag = true;
			item.ModifiedDate = DateTime.Now;
			item.ModifiedUser = request.RequestData.ModifiedUser;
		}

		_context.Users.UpdateRange(query);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<bool>.Success(true);
	}
}
