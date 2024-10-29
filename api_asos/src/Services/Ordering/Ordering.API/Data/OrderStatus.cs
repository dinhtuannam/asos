﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data;

[Table("tb_order_status")]
public class OrderStatus : BaseEntity<string>
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public ICollection<Order>? Orders { set; get; }
}
