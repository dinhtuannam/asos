using Catalog.Application.Features.CategoryFeature.Dto;
using Catalog.Application.Features.SizeFeature.Dto;
namespace Catalog.Application.Features.SizeFeature.Queries;

public record Category_GetBySizeCategoryIdQuery(Guid SizeCategoryId) : IQuery<Result<CategoryDto>>;
public class Category_GetBySizeCategoryIdQueryHandler : IQueryHandler<Category_GetBySizeCategoryIdQuery, Result<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Category_GetBySizeCategoryIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CategoryDto>> Handle(Category_GetBySizeCategoryIdQuery request, CancellationToken cancellationToken)
    {

        var Category = await _unitOfWork.Sizes.Queryable()
                                      .Where(s => s.Id == request.SizeCategoryId)
                                      .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();

        return Result<CategoryDto>.Success(Category);
    }
}