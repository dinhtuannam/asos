using Catalog.Application.Features.WishListFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.WishListFeature.Commands;

public record WishList_AddCommand(WishList RequestData) : ICommand<Result<WishListDto>>;

public class WishListAddCommandValidator : AbstractValidator<WishList_AddCommand>
{
    public WishListAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}

public class WishList_AddCommandHandler : ICommandHandler<WishList_AddCommand, Result<WishListDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishList_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<WishListDto>> Handle(WishList_AddCommand request, CancellationToken cancellationToken)
    {
        var wishlist= new WishList()
        {
            Id = request.RequestData.Id,
            ProductId = request.RequestData.ProductId,
            UserId= request.RequestData.UserId
        };

        _unitOfWork.Wishlists.Add(wishlist, request.RequestData.CreatedUser);
        await _unitOfWork.CompleteAsync();

        return Result<WishListDto>.Success(_mapper.Map<WishListDto>(wishlist));
    }
}