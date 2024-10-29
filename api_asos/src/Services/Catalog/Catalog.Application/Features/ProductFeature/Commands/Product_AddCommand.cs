using Catalog.Application.Features.ProductFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.ProductFeature.Commands;

public record Product_AddCommand(Product RequestData) : ICommand<Result<ProductDto>>;
public class Product_AddCommandHandler : ICommandHandler<Product_AddCommand, Result<ProductDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public Product_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public class Product_AddCommandValidator : AbstractValidator<Product_AddCommand>
	{
		public Product_AddCommandValidator()
		{
			RuleFor(command => command.RequestData.Id)
				.NotEmpty().WithMessage("Id is required");

			RuleFor(command => command.RequestData.Slug)
				.NotEmpty().WithMessage("Slug is required");

			RuleFor(command => command.RequestData.Name)
				.NotEmpty().WithMessage("Name is required");
			RuleFor(command => command.RequestData.AverageRating)
				.NotEmpty().WithMessage("AverageRating ");
			RuleFor(command => command.RequestData.Description)
				.NotEmpty().WithMessage("Description");

		}
	}
	public async Task<Result<ProductDto>> Handle(Product_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Products.IsSlugUnique(request.RequestData.Slug, true);

		var product = new Product()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			AverageRating = request.RequestData.AverageRating,

		};

		_unitOfWork.Products.Add(product, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
	}
}
