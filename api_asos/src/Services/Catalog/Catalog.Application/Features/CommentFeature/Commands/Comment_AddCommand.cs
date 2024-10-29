using Catalog.Application.Features.CommentFeature.Dto;
using Catalog.Application.Features.GenderFeature.Commands;
using Catalog.Application.Features.GenderFeature.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.CommentFeature.Commands;
public record Comment_AddCommand(Comment RequestData) : ICommand<Result<CommentDto>>;

public class CommentAddCommandValidator : AbstractValidator<Comment_AddCommand>
{
    public CommentAddCommandValidator()
    {
        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(command => command.RequestData.Content)
            .NotEmpty().WithMessage("Content is required");

        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("Product is required");
    }
}
public class Comment_AddCommandHandler : ICommandHandler<Comment_AddCommand, Result<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Comment_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<CommentDto>> Handle(Comment_AddCommand request, CancellationToken cancellationToken)
    {   
        var product = await _unitOfWork.Products.Queryable().Where(s => s.Id == request.RequestData.ProductId).FirstOrDefaultAsync();
        if (product == null) {
            throw new ApplicationException("Product not found");
        }

        var comment = new Comment()
        {
            UserId = request.RequestData.UserId,
            Content = request.RequestData.Content,
            ProductId = request.RequestData.ProductId
        };
        if (request.RequestData.ParentId != null)
        { 
           var parent = await _unitOfWork.Comments.Queryable().Where(s => s.Id == request.RequestData.ParentId.Value).FirstOrDefaultAsync();
            if (parent == null) {
                throw new ApplicationException("ParentId not found");
            }
        }


        _unitOfWork.Comments.Add(comment, request.RequestData.CreatedUser);
        await _unitOfWork.CompleteAsync();

        return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

    }
}
