
namespace Catalog.Application.Features.CommentFeature.Dto;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string ParentId { get; set; }
    public string UserId { get; set; }
    public string ProductId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}
