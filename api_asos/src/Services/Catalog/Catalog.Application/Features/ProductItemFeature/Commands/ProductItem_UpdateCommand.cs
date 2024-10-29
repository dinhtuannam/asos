using Catalog.Application.Features.ProductItemFeature.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_UpdateCommand(Guid Id, ProductItem RequestData, IFormFile? Image) : ICommand<Result<ProductItemDto>>;
public class ProductItem_UpdateCommandValidator : AbstractValidator<ProductItem_UpdateCommand>
{
	public ProductItem_UpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");
	}
}
public class ProductItem_UpdateCommandHandler : ICommandHandler<ProductItem_UpdateCommand, Result<ProductItemDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileStorageServices _fileStorageServices;
	private readonly IMapper _mapper;

	public ProductItem_UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageServices fileStorageServices)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_fileStorageServices = fileStorageServices;
	}

	public class ProductItem_UpdateCommandValidator : AbstractValidator<ProductItem_UpdateCommand>
	{
		public ProductItem_UpdateCommandValidator()
		{
			RuleFor(command => command.Id)
				.NotEmpty().WithMessage("Id is required");

			RuleFor(command => command.RequestData.OriginalPrice)
				.NotEmpty().WithMessage("OriginalPrice is required");
			RuleFor(command => command.RequestData.SalePrice)
			   .NotEmpty().WithMessage("SalePrice is required");
		}
	}

	public async Task<Result<ProductItemDto>> Handle(ProductItem_UpdateCommand request, CancellationToken cancellationToken)
	{


		var productItem = await _unitOfWork.ProductItems.FindAsync(request.RequestData.Id, true);
		if (productItem == null) return Result<ProductItemDto>.Failure("Product not found");


		productItem.SalePrice = request.RequestData.SalePrice;
		productItem.OriginalPrice = request.RequestData.OriginalPrice;
		productItem.IsSale = request.RequestData.IsSale;

		if (request.Image != null)
		{
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
			using var stream = request.Image.OpenReadStream();
			await _fileStorageServices.SaveFileAsync(stream, fileName);

			var imageUrl = $"images/{fileName}";
			var imageEntity = new Image
			{
				ProductItemId = productItem.Id,
				Url = imageUrl
			};

			var existingImages = await _unitOfWork.Images.GetImagesByProductItemIdAsync(productItem.Id);
			if (existingImages.Any())
			{
				var existingImage = existingImages.First();
				existingImage.Url = imageUrl;
			}
			else
			{
				_unitOfWork.Images.Add(imageEntity);
			}
		}

		_unitOfWork.ProductItems.Update(productItem, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<ProductItemDto>.Success(_mapper.Map<ProductItemDto>(productItem));
	}
}
