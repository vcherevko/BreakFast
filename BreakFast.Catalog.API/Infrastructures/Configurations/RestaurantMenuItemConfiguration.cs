using BreakFast.Catalog.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakFast.Catalog.API.Infrastructures.Configurations;

public class RestaurantMenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
	public void Configure(EntityTypeBuilder<MenuItem> builder)
	{
		builder.ToTable(nameof(MenuItem));
		builder.HasKey(menuItem => menuItem.Id);

		builder.Property(menuItem => menuItem.Id).ValueGeneratedOnAdd();
		builder.Property(menuItem => menuItem.Description).IsRequired(false);
		builder.Property(menuItem => menuItem.Name).IsRequired();
		builder.Property(menuItem => menuItem.RestaurantId).IsRequired();
		builder.Property(menuItem => menuItem.Price).IsRequired();
		builder.Property(menuItem => menuItem.ImagePath).IsRequired(false);
		builder.Property(menuItem => menuItem.IsAvailable).IsRequired();
		builder.Property(menuItem => menuItem.IsDeleted).HasDefaultValue(false);
	}
}
