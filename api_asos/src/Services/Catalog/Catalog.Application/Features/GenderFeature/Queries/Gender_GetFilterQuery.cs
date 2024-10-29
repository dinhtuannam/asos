using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.GenderFeature.Queries;

// Nếu RequestData đơn giản thì dùng FilterRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class FilterRequest
// Ví dụ : Catalog.Application/Models/GenderModel/GenderFilterRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Gender_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<GenderDto>>>;
public class Gender_GetFilterQueryHandler : IQueryHandler<Gender_GetFilterQuery, Result<IEnumerable<GenderDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GenderDto>>> Handle(Gender_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Genders.Queryable().OrderedListQuery(orderCol, orderDir)
							.ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
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

		return Result<IEnumerable<GenderDto>>.Success(await query.ToListAsync());
	}
}