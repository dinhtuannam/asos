using Catalog.Application.Features.CommentFeature.Dto;
using Catalog.Application.Features.GenderFeature.Dto;
using Catalog.Application.Features.GenderFeature.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.CommentFeature.Queries;

public record Comment_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<CommentDto>>>;
public class Comment_GetFilterQueryHandler : IQueryHandler<Comment_GetFilterQuery, Result<IEnumerable<CommentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_GetFilterQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CommentDto>>> Handle(Comment_GetFilterQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _unitOfWork.Comments.Queryable().OrderedListQuery(orderCol, orderDir)
                            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Content.Contains(request.RequestData.TextSearch));
        }

        if (request.RequestData.Skip != null)
        {
            query = query.Skip(request.RequestData.Skip.Value);
        }

        if (request.RequestData.TotalRecord != null)
        {
            query = query.Take(request.RequestData.TotalRecord.Value);
        }

        return Result<IEnumerable<CommentDto>>.Success(await query.ToListAsync());
    }
}