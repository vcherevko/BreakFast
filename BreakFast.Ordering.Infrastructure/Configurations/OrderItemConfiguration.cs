using BreakFast.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakFast.Ordering.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.ToTable(nameof(OrderItem));
		builder.HasKey(orderItem => orderItem.Id);
		builder.Property(orderItem => orderItem.Id).ValueGeneratedOnAdd();

		builder.Property(orderItem => orderItem.Price).IsRequired();
		builder.Property(orderItem => orderItem.RestaurantMenuItemId).IsRequired();
		builder.Property(orderItem => orderItem.OrderId).IsRequired();
		builder.Property(orderItem => orderItem.Quantity).IsRequired();
	}
}
