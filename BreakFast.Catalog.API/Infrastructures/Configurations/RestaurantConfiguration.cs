using BreakFast.Catalog.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakFast.Catalog.API.Infrastructures.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
	public void Configure(EntityTypeBuilder<Restaurant> builder)
	{
		builder.ToTable(nameof(Restaurant));
		builder.HasKey(restaurant => restaurant.Id);

		builder.Property(restaurant => restaurant.Id).ValueGeneratedOnAdd();
		builder.Property(restaurant => restaurant.Name).HasMaxLength(60).IsRequired();
		builder.Property(restaurant => restaurant.Address).HasMaxLength(100).IsRequired();
		builder.Property(restaurant => restaurant.Email).HasMaxLength(60).IsRequired();
		builder.Property(restaurant => restaurant.PhoneNumber).HasMaxLength(15).IsRequired();
		builder.Property(restaurant => restaurant.Status).IsRequired();
		builder.Property(restaurant => restaurant.IsDeleted).HasDefaultValue(false);

		builder.HasMany(restaurant => restaurant.MenuItems)
			.WithOne(menuItem => menuItem.Restaurant)
			.HasForeignKey(menuItem => menuItem.RestaurantId)
			.HasPrincipalKey(restaurant => restaurant.Id)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
