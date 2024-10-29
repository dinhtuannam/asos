namespace Catalog.Application.Features.WishListFeature.Dto;
public class WishListDto
{
    public Guid Id { get; set; }
    public string? ProductId { get; set; }
    public Product? Product { get; set; }
    public string? UserId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<WishList, WishListDto>();
        }
    }
}
