
using Catalog.Application.Features.ProductFeature.Dto;
using Catalog.Application.Features.VariationFeature.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Commands
{
   
    public record Variation_UpdateCommand(Variation RequestData) : ICommand<Result<VariationDto>>;
    public class Variation_UpdateCommandValidator : AbstractValidator<Variation_UpdateCommand>
    {
        public Variation_UpdateCommandValidator()
        {
            RuleFor(command => command.RequestData.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(command => command.RequestData.QtyDisplay)
                .NotEmpty().WithMessage("Slug is required");

            RuleFor(command => command.RequestData.QtyInStock)
                .NotEmpty().WithMessage("Name is required");

        }
    }
    public class Product_UpdateCommandHandler : ICommandHandler<Variation_UpdateCommand, Result<VariationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Product_UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<VariationDto>> Handle(Variation_UpdateCommand request, CancellationToken cancellationToken)
        {
            var variation = await _unitOfWork.Variations.FindAsync(request.RequestData.Id, true);

            if (variation != null)
            {
                variation.QtyDisplay = request.RequestData.QtyDisplay;
                variation.QtyInStock = request.RequestData.QtyInStock;
                variation.Stock = request.RequestData.Stock;
                _unitOfWork.Variations.Update(variation, request.RequestData.ModifiedUser);
                await _unitOfWork.CompleteAsync();
            }
            return Result<VariationDto>.Success(_mapper.Map<VariationDto>(variation));
        }
    }
}
