namespace BreakFast.Ordering.API.DTOs;

public record OrderCreatingDto(int RestaurantId, IEnumerable<OrderItemCreatingDto> OrderItems);
