using Catalog.Application.Features.SizeFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.SizeFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/SizeModel/SizeAddRequest.cs
public record Size_AddCommand(Size RequestData) : ICommand<Result<SizeDto>>;

public class SizeAddCommandValidator : AbstractValidator<Size_AddCommand>
{
	public SizeAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

        RuleFor(command => command.RequestData.SizeCategories)
            .NotEmpty().WithMessage("Size categories is required");
    }
}

public class Size_AddCommandHandler : ICommandHandler<Size_AddCommand, Result<SizeDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Sizes.IsSlugUnique(request.RequestData.Slug, true);

		var Size = new Size()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			SizeCategories = request.RequestData.SizeCategories
		};

		_unitOfWork.Sizes.Add(Size, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(Size));
	}
}