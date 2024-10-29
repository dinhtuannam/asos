using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

// Nếu RequestData đơn giản thì dùng FilterRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class FilterRequest
// Ví dụ : Catalog.Application/Models/SizeModel/SizeFilterRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Size_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetFilterQueryHandler : IQueryHandler<Size_GetFilterQuery, Result<IEnumerable<SizeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Sizes.Queryable().OrderedListQuery(orderCol, orderDir)
							.ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

		return Result<IEnumerable<SizeDto>>.Success(await query.ToListAsync());
	}
}