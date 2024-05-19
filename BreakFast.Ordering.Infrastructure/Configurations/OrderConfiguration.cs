using BreakFast.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakFast.Ordering.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable(nameof(Order));
		builder.HasKey(order => order.Id);

		builder.Property(order => order.Id).ValueGeneratedOnAdd();
		builder.Property(order => order.CreatedAt);
		builder.Property(order => order.ChangedAt);
		builder.Property(order => order.Timestamp).IsConcurrencyToken();
		builder.Property(order => order.Status).IsRequired();
		builder.Property(order => order.ConsumerId).IsRequired();
		builder.Property(order => order.RestaurantId).IsRequired();
		builder.Property(order => order.CourierId).IsRequired(false);

		builder.HasMany(order => order.OrderItems)
			.WithOne(orderItem => orderItem.Order)
			.HasForeignKey(orderItem => orderItem.OrderId)
			.HasPrincipalKey(order => order.Id)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
