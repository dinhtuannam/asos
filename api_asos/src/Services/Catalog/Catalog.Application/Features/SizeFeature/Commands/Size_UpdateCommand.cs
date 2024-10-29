using Catalog.Application.Features.SizeFeature.Dto;
using FluentValidation;

namespace Catalog.Application.Features.SizeFeature.Commands;

// Nếu RequestData đơn giản thì dùng JsonIgnore với các Column ko cần thiết , 
// Còn khi RequestData phức tạp hơn thì tạo trong thư mục Catalog.Application/Models
// Ví dụ : Catalog.Application/Models/SizeModel/SizeUpdateRequest.cs
public record Size_UpdateCommand(Size RequestData) : ICommand<Result<SizeDto>>;

public class SizeUpdateCommandValidator : AbstractValidator<Size_UpdateCommand>
{
	public SizeUpdateCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Slug)
			.NotEmpty().WithMessage("Slug is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

        RuleFor(command => command.RequestData.SizeCategories)
            .NotEmpty().WithMessage("Size Category is required");
    }
}

public class Size_UpdateCommandHandler : ICommandHandler<Size_UpdateCommand, Result<SizeDto>>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public Size_UpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_UpdateCommand request, CancellationToken cancellationToken)
	{
		var Size = await _unitOfWork.Sizes.FindAsync(request.RequestData.Id, true);

		if(Size!.Slug != request.RequestData.Slug)
		{
			var exist = await _unitOfWork.Sizes.Queryable()
										 .Where(s => s.Slug == request.RequestData.Slug
												  && s.Id != Size.Id)
										 .FirstOrDefaultAsync();
			if (exist != null)
			{
				throw new ApplicationException($"Slug already in use : {request.RequestData.Slug}");
			}
			Size.Slug = request.RequestData.Slug;
		}

		Size.Name = request.RequestData.Name;
		Size.Description = request.RequestData.Description;
		Size.SizeCategories = request.RequestData.SizeCategories;

		_unitOfWork.Sizes.Update(Size, request.RequestData.ModifiedUser);
		await _unitOfWork.CompleteAsync();

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(Size));
	}
}