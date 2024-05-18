namespace BreakFast.Ordering.Domain.Exceptions;

public class MenuItemsAvailabilityException : BreakFastAppException
{
	public MenuItemsAvailabilityException()
	{
	}

	public MenuItemsAvailabilityException(string message)
		: base(message)
	{
	}

	public MenuItemsAvailabilityException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
