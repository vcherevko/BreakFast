namespace BreakFast.Ordering.API.DTOs;

public class OrderDto
{
	public int Id { get; set; }

	public int RestaurantId { get; set; }

	public int? CourierId { get; set; }

	public required string Status { get; set; }

	public DateTime CreatedAt { get; set; }

	public double TotalPrice { get; set; }

	public required ICollection<OrderItemDto> OrderItems { get; set; }
}
