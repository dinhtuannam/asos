﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.CommentFeature.Commands;

public record Comment_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Comment_DeleteCommandHandler : ICommandHandler<Comment_DeleteCommand, Result<bool>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_DeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(Comment_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        IEnumerable<Guid> ids = request.RequestData.Ids.Select(m => Guid.Parse(m)).ToList();
        var comments = await _unitOfWork.Comments.FindByIds(ids, true);

        _unitOfWork.Comments.SoftDeleteRange(comments, request.RequestData.ModifiedUser);

        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true);
    }
}
