using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;

public record Category_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<CategoryDto>>>;
public class Category_GetAllQueryHandler : IQueryHandler<Category_GetAllQuery, Result<IEnumerable<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CategoryDto>>> Handle(Category_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<CategoryDto> Categorys = await _unitOfWork.Categories.Queryable()
											   .OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<CategoryDto>>.Success(Categorys);
	}
}