﻿namespace Catalog.Application.Features.ProductFeature.Commands;

public record Product_DeleteCommand(DeleteRequest DeleteRequest) : ICommand<Result<bool>>;
public class Product_DeleteCommandHandler : ICommandHandler<Product_DeleteCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitofWork;
	private readonly IMapper _mapper;
	public Product_DeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitofWork = unitOfWork;
		_mapper = mapper;
	}
	public async Task<Result<bool>> Handle(Product_DeleteCommand request, CancellationToken cancellationToken)
	{

		if (request.DeleteRequest.Ids == null)
			throw new ApplicationException("Ids not found");

		IEnumerable<Guid> ids = request.DeleteRequest.Ids.Select(m => Guid.Parse(m)).ToList();
		var products = await _unitofWork.Products.FindByIds(ids, true);

		_unitofWork.Products.SoftDeleteRange(products, request.DeleteRequest.ModifiedUser);

		await _unitofWork.CompleteAsync();
		return Result<bool>.Success(true);
	}
}
