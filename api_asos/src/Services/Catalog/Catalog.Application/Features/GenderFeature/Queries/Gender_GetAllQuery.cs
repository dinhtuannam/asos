using Catalog.Application.Features.GenderFeature.Dto;

namespace Catalog.Application.Features.GenderFeature.Queries;

// Nếu RequestData đơn giản thì dùng BaseRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class BaseRequest
// Ví dụ : Catalog.Application/Models/GenderModel/GenderGetAllRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Gender_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<GenderDto>>>;
public class Gender_GetAllQueryHandler : IQueryHandler<Gender_GetAllQuery, Result<IEnumerable<GenderDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Gender_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<GenderDto>>> Handle(Gender_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<GenderDto> Genders = await _unitOfWork.Genders.Queryable()
											   .OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<GenderDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<GenderDto>>.Success(Genders);
	}
}