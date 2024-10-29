using Catalog.Application.Features.CategoryFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.CategoryFeature.Commands;
public record Category_UpdateCommand(Category RequestData) : ICommand<Result<CategoryDto>>;

public class CategoryUpdateCommandValidator : AbstractValidator<Category_UpdateCommand>
{
	public CategoryUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

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

public class Category_UpdateCommandHandler : ICommandHandler<Category_UpdateCommand, Result<CategoryDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Category_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<CategoryDto>> Handle(Category_UpdateCommand request, CancellationToken cancellationToken)
	{
		var Category = await _unitOfWork.Categories.FindAsync(request.RequestData.Id, true);

		if(Category!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Categories.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != Category.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			Category.Slug = request.RequestData.Slug;
		}

		Category.Name = request.RequestData.Name;
		Category.Description = request.RequestData.Description;
		Category.ImageFile = request.RequestData.ImageFile;
		Category.ParentId = request.RequestData.ParentId;
		Category.SizeCategories = request.RequestData.SizeCategories;
		Category.GenderId = request.RequestData.GenderId;

		_unitOfWork.Categories.Update(Category, request.RequestData.ModifiedUser);
		await _unitOfWork.CompleteAsync();

		return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(Category));
	}
}