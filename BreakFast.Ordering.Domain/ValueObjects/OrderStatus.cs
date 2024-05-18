namespace BreakFast.Ordering.Domain.ValueObjects;

public enum OrderStatus
{
	ConsumerCreated,
	ConsumerPayed,
	ConsumerCanceled,

	RestaurantAccepted,
	RestaurantCompleted,
	RestaurantDenied,
	RestaurantRefunded,

	CourierAccepted,
	CourierDelivering,
	CourierCompleted,
	CourierDenied,
	CourierRefunded
}
