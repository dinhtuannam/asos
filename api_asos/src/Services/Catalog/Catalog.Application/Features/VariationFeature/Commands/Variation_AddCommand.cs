
using Catalog.Application.Features.ColorFeature.Dto;
using Catalog.Application.Features.VariationFeature.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Commands
{
 
    public record Variation_AddCommand(Variation RequestData) : ICommand<Result<VariationDto>>;
    public class VariationAddCommandValidator : AbstractValidator<Variation_AddCommand>
    {
        public VariationAddCommandValidator()
        {
            RuleFor(command => command.RequestData.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(command => command.RequestData.QtyDisplay)
                .NotEmpty().WithMessage("Slug is required");

            RuleFor(command => command.RequestData.QtyInStock)
                .NotEmpty().WithMessage("Name is required");
           
        }
    }
    public class Color_AddCommandHandler : ICommandHandler<Variation_AddCommand, Result<VariationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Color_AddCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VariationDto>> Handle(Variation_AddCommand request, CancellationToken cancellationToken)
        {
           // await _unitOfWork.Variations.IsSlugUnique(request.RequestData, true);
            var variations = new Variation()
            {
                QtyDisplay= request.RequestData.QtyDisplay,
                QtyInStock=request.RequestData.QtyInStock,
                Stock=request.RequestData.Stock
            };

            _unitOfWork.Variations.Add(variations, request.RequestData.CreatedUser);
            await _unitOfWork.CompleteAsync();

            return Result<VariationDto>.Success(_mapper.Map<VariationDto>(variations));
        }
    }
}
