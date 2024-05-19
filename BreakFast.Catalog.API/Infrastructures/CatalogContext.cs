using BreakFast.Catalog.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Catalog.API.Infrastructures;

public class CatalogContext : DbContext
{
	public CatalogContext(DbContextOptions<CatalogContext> options)
		: base(options)
	{
	}

	public DbSet<Restaurant> Restaurant { get; set; }

	public DbSet<MenuItem> MenuItem { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);

		SeedData(modelBuilder);
	}

	private void SeedData(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Restaurant>()
			.HasData(
				new Restaurant
				{
					Id = 1,
					Name = "Ukrainian Cuisine",
					Email = "ukrainiancuisine@gamil.com",
					Status = RestaurantStatus.Open,
					Address = "st. Second 1, Kiev",
					PhoneNumber = "+380773698741",
					MenuItems = new List<MenuItem>()
				},
				new Restaurant
				{
					Id = 2,
					Name = "Sushi Star",
					Email = "sushistar@gamil.com",
					Status = RestaurantStatus.Open,
					Address = "st. Main 198, Kiev",
					PhoneNumber = "+380778899445",
					MenuItems = new List<MenuItem>()
				},
				new Restaurant
				{
					Id = 3,
					Name = "HiTai",
					Email = "hitai@gamil.com",
					Status = RestaurantStatus.Closed,
					Address = "st. Narrow 33, Kiev",
					PhoneNumber = "+380777412369",
					MenuItems = new List<MenuItem>()
				});

		modelBuilder.Entity<MenuItem>()
			.HasData(
				new MenuItem
				{
					Id = 1,
					Name = "Borch with cream",
					RestaurantId = 1,
					Price = 85.55,
					Description = "Ukrainian Traditional meal",
					IsAvailable = true
				},
				new MenuItem
				{
					Id = 2,
					Name = "Vareniki",
					RestaurantId = 1,
					Price = 150,
					Description = "Original recipe",
					IsAvailable = true
				},
				new MenuItem
				{
					Id = 3,
					Name = "Sushi with avocado",
					RestaurantId = 2,
					Price = 190,
					Description = "Original recipe from Japan",
					IsAvailable = true
				},
				new MenuItem
				{
					Id = 4,
					Name = "Sushi California",
					RestaurantId = 2,
					Price = 230,
					Description = "Weight of set 500g.",
					IsAvailable = true
				},
				new MenuItem
				{
					Id = 5,
					Name = "Soup with Red Hot Chili Pepper",
					RestaurantId = 3,
					Price = 350,
					Description = "Very hot and unforgettable",
					IsAvailable = true
				},
				new MenuItem
				{
					Id = 6,
					Name = "Noodles with seafood",
					RestaurantId = 3,
					Price = 299.99,
					Description = "Very popular meal from Thailand",
					IsAvailable = true
				});
	}
}
