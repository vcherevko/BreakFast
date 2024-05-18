namespace BreakFast.Ordering.Domain.Entities;

public class OrderItem
{
	public int Id { get; set; }

	public int OrderId { get; set; }

	public Order Order { get; set; }

	public double Price { get; set; }

	public int Quantity { get; set; }

	public int RestaurantMenuItemId { get; set; }
}
