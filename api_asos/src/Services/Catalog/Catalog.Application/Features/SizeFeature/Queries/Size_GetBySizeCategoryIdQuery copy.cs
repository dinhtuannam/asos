using Catalog.Application.Features.SizeFeature.Dto;
namespace Catalog.Application.Features.SizeFeature.Queries;

public record Size_GetBySizeCategoryIdQuery(Guid SizeCategoryId) : IQuery<Result<SizeDto>>;
public class Size_GetBySizeCategoryIdQueryHandler : IQueryHandler<Size_GetBySizeCategoryIdQuery, Result<SizeDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetBySizeCategoryIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_GetBySizeCategoryIdQuery request, CancellationToken cancellationToken)
	{

		var Size = await _unitOfWork.Sizes.Queryable()
									  .Where(s => s.Id == request.SizeCategoryId)
									  .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
									  .FirstOrDefaultAsync();

		return Result<SizeDto>.Success(Size);
	}
}