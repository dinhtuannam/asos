using Catalog.Application.Features.WishListFeature.Dto;

namespace Catalog.Application.Features.WishListFeature.Queries;
public record WishList_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<WishListDto>>>;
public class WishList_GetFilterQueryHandler : IQueryHandler<WishList_GetFilterQuery, Result<IEnumerable<WishListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<WishListDto>>> Handle(WishList_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Wishlists.Queryable().OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<WishListDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Id.ToString().Contains(request.RequestData.TextSearch));
        }

        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<WishListDto>>.Success(await query.ToListAsync());
    }
}