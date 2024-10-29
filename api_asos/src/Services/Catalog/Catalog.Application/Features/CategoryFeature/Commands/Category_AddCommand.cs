using Catalog.Application.Features.CategoryFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.CategoryFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/CategoryModel/CategoryAddRequest.cs
public record Category_AddCommand(Category RequestData) : ICommand<Result<CategoryDto>>;

public class CategoryAddCommandValidator : AbstractValidator<Category_AddCommand>
{
	public CategoryAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

        RuleFor(command => command.RequestData.ParentId)
            .NotEmpty().WithMessage("Parent Id is required");

        RuleFor(command => command.RequestData.SizeCategories)
            .NotEmpty().WithMessage("Size Categories is required");

        RuleFor(command => command.RequestData.GenderId)
            .NotEmpty().WithMessage("Gender Id is required");

    }
}

public class Category_AddCommandHandler : ICommandHandler<Category_AddCommand, Result<CategoryDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_AddCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_AddCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Categories.IsSlugUnique(request.RequestData.Slug, true);

		var Category = new Category()
		{
			Slug = request.RequestData.Slug,
			Name = request.RequestData.Name,
			Description = request.RequestData.Description,
			ImageFile = request.RequestData.ImageFile,
			ParentId = request.RequestData.ParentId
		};

		_unitOfWork.Categories.Add(Category, request.RequestData.CreatedUser);
		await _unitOfWork.CompleteAsync();

		return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(Category));
	}
}