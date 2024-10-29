using BuildingBlock.Core.Paging;
using Catalog.Application.Features.ProductFeature.Dto;

namespace Catalog.Application.Features.ProductFeature.Queries;

public record Product_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<ProductDto>>>;
public class Product_GetPaginationHandler : IQueryHandler<Product_GetPaginationQuery, Result<PaginatedList<ProductDto>>>
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	public Product_GetPaginationHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}
	public async Task<Result<PaginatedList<ProductDto>>> Handle(Product_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var product = _unitOfWork.Products.Queryable()
							     .Where(p => p.BrandId != null && p.CategoryId != null)
								 .OrderedListQuery(orderCol, orderDir)
								 .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
								 .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			product = product.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await product.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		
		return Result<PaginatedList<ProductDto>>.Success(paging);
	}
}
