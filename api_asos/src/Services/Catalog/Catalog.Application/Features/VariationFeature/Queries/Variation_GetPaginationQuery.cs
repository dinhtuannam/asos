using BuildingBlock.Core.Paging;
using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Features.VariationFeature.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Queries
{
 
    public record Variation_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<VariationDto>>>;
    public class Product_GetPaginationHandler : IQueryHandler<Variation_GetPaginationQuery, Result<PaginatedList<VariationDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public Product_GetPaginationHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedList<VariationDto>>> Handle(Variation_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;
            var variations = _unitOfWork.Variations.Queryable()
                                              .Where(s => s.SizeId != null || s.ProductItemId != null)
                                             .OrderedListQuery(orderCol, orderDir)
                                             .ProjectTo<VariationDto>(_mapper.ConfigurationProvider)
                                             .AsNoTracking();
            if (!string.IsNullOrEmpty(request.RequestData.TextSearch) &&
     int.TryParse(request.RequestData.TextSearch, out int searchQtyInStock))
            {
                variations = variations.Where(s => s.QtyInStock == searchQtyInStock);
            }
            var paging = await variations.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
            return Result<PaginatedList<VariationDto>>.Success(paging);
        }
    }
}
