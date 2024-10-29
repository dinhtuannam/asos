using Catalog.Application.Features.SizeFeature.Dto;

namespace Catalog.Application.Features.SizeFeature.Queries;

// Nếu RequestData đơn giản thì dùng BaseRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class BaseRequest
// Ví dụ : Catalog.Application/Models/SizeModel/SizeGetAllRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Size_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetAllQueryHandler : IQueryHandler<Size_GetAllQuery, Result<IEnumerable<SizeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<SizeDto> Sizes = await _unitOfWork.Sizes.Queryable()
											   .OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<SizeDto>>.Success(Sizes);
	}
}