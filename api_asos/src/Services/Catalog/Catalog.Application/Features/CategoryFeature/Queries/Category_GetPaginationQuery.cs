﻿using BuildingBlock.Core.Paging;
using Catalog.Application.Features.CategoryFeature.Dto;

namespace Catalog.Application.Features.CategoryFeature.Queries;
public record Category_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<CategoryDto>>>;
public class Category_GetPaginationQueryHandler : IQueryHandler<Category_GetPaginationQuery, Result<PaginatedList<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_GetPaginationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<CategoryDto>>> Handle(Category_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _unitOfWork.Categories.Queryable()
							   .OrderedListQuery(orderCol, orderDir)
							   .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
							   .AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<CategoryDto>>.Success(paging);
	}
}