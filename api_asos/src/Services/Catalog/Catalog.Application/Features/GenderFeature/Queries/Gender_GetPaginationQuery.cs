using BuildingBlock.Core.Paging;
using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.GenderFeature.Queries;

// Nếu RequestData đơn giản thì dùng PaginationRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class PaginationRequest
// Ví dụ : Catalog.Application/Models/GenderModel/GenderPaginationRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Gender_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<GenderDto>>>;
public class Gender_GetPaginationQueryHandler : IQueryHandler<Gender_GetPaginationQuery, Result<PaginatedList<GenderDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<GenderDto>>> Handle(Gender_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Genders.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<GenderDto>>.Success(paging);
	}
}