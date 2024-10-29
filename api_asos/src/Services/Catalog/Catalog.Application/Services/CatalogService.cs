using BuildingBlock.Grpc.Protos;
using Grpc.Core;

namespace Catalog.Application.Services;

public class CatalogService : CatalogGrpc.CatalogGrpcBase
{
	private readonly IUnitOfWork _unitOfWork;
	public CatalogService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public override async Task<GetProductReply> GetProduct(GetProductRequest request, ServerCallContext context)
	{
		var variation = await _unitOfWork.Variations.Queryable()
							  .Where(s => s.Id == Guid.Parse(request.Id) &&
										  s.ProductItem != null && s.ProductItem.Product != null &&
										  s.ProductItem.Color != null && s.Size != null && 
										  s.ProductItem.Product.Brand != null && s.ProductItem.Product.Category != null)
							  .Select(s => new GetProductReply()
							  {
								  Success = true,
								  ErrMessage = "",
								  VariationId = s.Id.ToString(),
								  ProductId = s.ProductItem!.ProductId.ToString(),
								  ProductItemId = s.ProductItemId.ToString(),
								  Slug = s.ProductItem.Product!.Slug,
								  Name = s.ProductItem.Product!.Name,
								  Description = "",
								  Category = s.ProductItem.Product!.Category!.Name,
								  Brand = s.ProductItem.Product!.Brand!.Name,
								  Size = s.Size!.Name,
								  Color = s.ProductItem.Color!.Name,
								  OriginalPrice = (double)s.ProductItem.OriginalPrice,
								  SalePrice = (double)s.ProductItem.SalePrice,
								  Stock = (double)s.Stock,
								  IsSale = s.ProductItem.IsSale,
								  QtyDisplay = s.QtyDisplay,
								  QtyInStock = s.QtyInStock,
								  Image = s.ProductItem.Images == null ? "" : 
										  s.ProductItem.Images.Select(s => s.Url).FirstOrDefault()
							  })
							  .FirstOrDefaultAsync();

		if (variation != null)
		{
			return variation;
		}

		return new GetProductReply()
		{
			Success = false,
			ErrMessage = "Product not found."
		};
	}
}