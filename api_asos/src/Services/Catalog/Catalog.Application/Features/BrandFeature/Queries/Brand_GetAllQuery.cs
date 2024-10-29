using Catalog.Application.Features.BrandFeature.Dto;

namespace Catalog.Application.Features.BrandFeature.Queries;

// Nếu RequestData đơn giản thì dùng BaseRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class BaseRequest
// Ví dụ : Catalog.Application/Models/BrandModel/BrandGetAllRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Brand_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<BrandDto>>>;
public class Brand_GetAllQueryHandler : IQueryHandler<Brand_GetAllQuery, Result<IEnumerable<BrandDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<BrandDto>>> Handle(Brand_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<BrandDto> Brands = await _unitOfWork.Brands.Queryable()
											   .OrderedListQuery(orderCol, orderDir)
											   .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
											   .ToListAsync();

		return Result<IEnumerable<BrandDto>>.Success(Brands);
	}
}