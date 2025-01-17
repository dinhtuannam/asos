﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Data.Configurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
	public void Configure(EntityTypeBuilder<OrderStatus> builder)
	{
		builder.HasQueryFilter(p => p.DeleteFlag != true);
	}
}
