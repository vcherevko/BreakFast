namespace BreakFast.Ordering.API.DTOs;

public record OrderItemCreatingDto(double Price, int Quantity, int RestaurantMenuItemId);
