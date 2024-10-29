using Catalog.Application.Features.ProductFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.ProductFeature.Commands
{
     public record Product_UpdateCommand(Product RequestData):ICommand<Result<ProductDto>>;
    public class Product_UpdateCommandValidator : AbstractValidator<Product_UpdateCommand>
    {
        public Product_UpdateCommandValidator()
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
    public class Product_UpdateCommandHandler : ICommandHandler<Product_UpdateCommand, Result<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Product_UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       
        public async Task<Result<ProductDto>> Handle(Product_UpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.FindAsync(request.RequestData.Id, true);

            if (product!.Slug != request.RequestData.Slug)
            {
                var exist = await _unitOfWork.Products.Queryable()
                                             .Where(s => s.Slug == request.RequestData.Slug
                                                      && s.Id != product.Id)
                                             .FirstOrDefaultAsync();
                if (exist != null)
                {
                    throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
                }
                product.Slug = request.RequestData.Slug;
            }

            product.Name = request.RequestData.Name;
            product.Description = request.RequestData.Description;

            _unitOfWork.Products.Update(product, request.RequestData.ModifiedUser);
            await _unitOfWork.CompleteAsync();

            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }
    }

}
