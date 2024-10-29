﻿namespace Catalog.Domain.Entities;

[Table("tb_categories")]
public class Category : BaseEntity<Guid>
{
	public Category() : base() { }
	public string Slug { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public string? ImageFile { get; set; } = string.Empty;
	public Guid? ParentId { get; set; }
	public Guid? GenderId { get; set; }
	[JsonIgnore] public Gender? Gender { get; set; }
	[JsonIgnore] public virtual ICollection<Product>? Products { get; set; }
	[JsonIgnore] public virtual ICollection<SizeCategory>? SizeCategories { get; set; }

}
