using Catalog.Application.Features.CommentFeature.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.CommentFeature.Queries;
public record Comment_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<CommentDto>>>;
public class Comment_GetAllQueryHandler : IQueryHandler<Comment_GetAllQuery, Result<IEnumerable<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<CommentDto>>> Handle(Comment_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<CommentDto> Comments = await _unitOfWork.Comments.Queryable()
                                               .OrderedListQuery(orderCol, orderDir)
                                               .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync();
        return Result<IEnumerable<CommentDto>>.Success(Comments);
    }
}
