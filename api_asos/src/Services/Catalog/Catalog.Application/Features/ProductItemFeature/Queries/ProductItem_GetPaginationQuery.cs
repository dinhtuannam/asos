using BuildingBlock.Core.Paging;
using Catalog.Application.Features.ProductItemFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.ProductItemFeature.Queries
{

    public record ProductItem_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<ProductItemDto>>>;
    public class ProductItem_GetPaginationHandler : IQueryHandler<ProductItem_GetPaginationQuery, Result<PaginatedList<ProductItemDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductItem_GetPaginationHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<PaginatedList<ProductItemDto>>> Handle(ProductItem_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;
            var product = _unitOfWork.ProductItems.Queryable()
                                             .Where(p => p.ProductId != null && p.ColorId != null)
                                             .OrderedListQuery(orderCol, orderDir)
                                             .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider)
                                             .AsNoTracking();

            var paging = await product.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
            return Result<PaginatedList<ProductItemDto>>.Success(paging);
        }
    }
}
