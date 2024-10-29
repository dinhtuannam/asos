using Catalog.Application.Features.ProductItemFeature.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Catalog.Application.Features.ProductItemFeature.Commands;

public record ProductItem_AddCommand(ProductItem RequestData, IFormFile? image) : ICommand<Result<ProductItemDto>>;
public class Product_AddCommandHandler : ICommandHandler<ProductItem_AddCommand, Result<ProductItemDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFileStorageServices _fileStorageServices;
	private readonly IMapper _mapper;
	public Product_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageServices fileStorageServices)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_fileStorageServices = fileStorageServices;
	}
	public class Product_AddCommandValidator : AbstractValidator<ProductItem_AddCommand>
	{
		public Product_AddCommandValidator()
		{
			RuleFor(command => command.RequestData.Id)
				.NotEmpty().WithMessage("Id is required");

			RuleFor(command => command.RequestData.IsSale)
				.NotEmpty().WithMessage("IsSale is required");

			RuleFor(command => command.RequestData.OriginalPrice)
				.NotEmpty().WithMessage("OriginalPrice is required");


		}
	}
	public async Task<Result<ProductItemDto>> Handle(ProductItem_AddCommand request, CancellationToken cancellationToken)
	{
		var productItem = new ProductItem()
		{
			SalePrice = request.RequestData.SalePrice,
			OriginalPrice = request.RequestData.OriginalPrice,
			IsSale = request.RequestData.IsSale,

		};
		if (request.image != null)
		{
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.image.FileName)}";
			using var stream = request.image.OpenReadStream();
			await _fileStorageServices.SaveFileAsync(stream, fileName);

			var imageUrl = $"images/{fileName}";
			var imageEntity = new Image
			{
				ProductItemId = productItem.Id,
				Url = imageUrl
			};
			_unitOfWork.ProductItems.Add(productItem, request.RequestData.CreatedUser);
			await _unitOfWork.CompleteAsync();
		}
		return Result<ProductItemDto>.Success(_mapper.Map<ProductItemDto>(productItem));

	}
}
