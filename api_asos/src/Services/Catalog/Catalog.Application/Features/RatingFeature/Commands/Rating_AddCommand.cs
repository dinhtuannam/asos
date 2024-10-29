using Catalog.Application.Features.RatingFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.RatingFeature.Commands;
public record Rating_AddCommand(Rating RequestData) : ICommand<Result<RatingDto>>;

public class RatingAddCommandValidator : AbstractValidator<Rating_AddCommand>
{
    public RatingAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(command => command.RequestData.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(command => command.RequestData.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}

public class Rating_AddCommandHandler : ICommandHandler<Rating_AddCommand, Result<RatingDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Rating_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RatingDto>> Handle(Rating_AddCommand request, CancellationToken cancellationToken)
    {
        var rating = new Rating()
        {
            ProductId = request.RequestData.ProductId,
            UserId = request.RequestData.UserId,
            Rate = request.RequestData.Rate
        };

        var product = await _unitOfWork.Products.FindAsync(request.RequestData.ProductId!.Value);
        if (product == null)
        {
            return Result<RatingDto>.Failure("Product not found");
        }

       /* var totalRatings = product.RatingCount; 
        var totalScore = product.AverageRating * totalRatings; 

        totalRatings += 1;
        totalScore += rating.Rate;

        product.AverageRating = totalScore / totalRatings;
        product.RatingCount = totalRatings;*/

        _unitOfWork.Ratings.Add(rating, request.RequestData.CreatedUser);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();

        return Result<RatingDto>.Success(_mapper.Map<RatingDto>(rating));
    }

}