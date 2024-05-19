using BreakFast.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Ordering.Infrastructure;

public class OrderingContext : DbContext
{
	public OrderingContext(DbContextOptions options)
		: base(options)
	{
	}

	public DbSet<Order> Order { get; set; }

	public DbSet<OrderItem> OrderItem { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingContext).Assembly);

		SeedData(modelBuilder);
	}

	private void SeedData(ModelBuilder modelBuilder)
	{
		/*modelBuilder.Entity<Order>()
			.HasData(
				new Order()
				{

				},
				new Order()
				{

				});*/
	}
}
