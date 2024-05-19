namespace BreakFast.Catalog.API.Domain.Models;

public class MenuItem
{
	public int Id { get; set; }

	public required string Name { get; set; }

	public int RestaurantId { get; set; }

	public Restaurant Restaurant { get; set; }

	public double Price { get; set; }

	public string? Description { get; set; }

	public bool IsAvailable { get; set; }

	public string? ImagePath { get; set; }

	public bool IsDeleted { get; set; }
}
