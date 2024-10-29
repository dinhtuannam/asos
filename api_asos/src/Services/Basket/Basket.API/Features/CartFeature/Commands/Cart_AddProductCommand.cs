using Basket.API.Features.CartFeature.Dto;

namespace Basket.API.Features.CartFeature.Commands;

public record Cart_AddProductCommand(CartAddOrDeleteProductRequest RequestData) : ICommand<Result<CartDto>>;

public class CartAddCommandValidator : AbstractValidator<Cart_AddProductCommand>
{
	public CartAddCommandValidator()
	{

		RuleFor(command => command.RequestData.UserId).NotEmpty().WithMessage("User not found");

		RuleFor(command => command.RequestData.VariationId).NotEmpty().WithMessage("Product not found");
	}
}

public class Cart_AddProductCommandHandler : ICommandHandler<Cart_AddProductCommand, Result<CartDto>>
{
	private readonly ICartService _cartService;

	public Cart_AddProductCommandHandler(ICartService cartService)
	{
		_cartService = cartService;
	}

	public async Task<Result<CartDto>> Handle(Cart_AddProductCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartService.GetCart(request.RequestData.UserId);

		var variation = cart.Items.Where(s => s.VariationId == request.RequestData.VariationId)
							.FirstOrDefault();

		if (!cart.Items.Any() || variation == null)
		{
			var cartItem = new CartItemDto
			{
				VariationId = request.RequestData.VariationId,
				Slug = "product new",
				Name = "Product new",
				Description = "This is the first product",
				Category = "Electronics",
				Brand = "Brand A",
				Size = "M",
				Color = "Red",
				OriginalPrice = 100,
				SalePrice = 50,
				Stock = 0,
				IsSale = false,
				Quantity = 2,
				Image = "productNew.jpg"
			};
			cart.Items.Add(cartItem);
		}
		else
		{
			variation!.Quantity++;
		}

		cart.ProcessData();
		await _cartService.SetCache(cart);

		return Result<CartDto>.Success(cart);
	}
}