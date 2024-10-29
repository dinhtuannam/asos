namespace Promotion.API.Models;

public class DiscountAddOrUpdateRequest : AddOrUpdateRequest
{
    public Guid? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Code { get; set; }
    public decimal Value { get; set; }
    public decimal Minimum { get; set; } 
    public string DiscountTypeId { get; set; }
}
