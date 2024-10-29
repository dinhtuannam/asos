using Catalog.Application.Features.BrandFeature.Dto;

namespace Catalog.Application.Features.BrandFeature.Queries;

// Nếu RequestData đơn giản thì dùng FilterRequest , 
// Còn khi RequestData cần thêm các param khác để filter
// thì tạo trong thư mục Catalog.Application/Models và kế thừa lại class FilterRequest
// Ví dụ : Catalog.Application/Models/BrandModel/BrandFilterRequest.cs
// Tuyệt đối ko sửa code các class ở tầng BuildingBlock
public record Brand_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<BrandDto>>>;
public class Brand_GetFilterQueryHandler : IQueryHandler<Brand_GetFilterQuery, Result<IEnumerable<BrandDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Brand_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<BrandDto>>> Handle(Brand_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Brands.Queryable().OrderedListQuery(orderCol, orderDir)
							.ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
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

		return Result<IEnumerable<BrandDto>>.Success(await query.ToListAsync());
	}
}