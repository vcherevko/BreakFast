namespace BreakFast.Ordering.Domain.Exceptions;

public class OrderAccessDeniedException : BreakFastAppException
{
	public OrderAccessDeniedException()
		: base("Order access denied.")
	{
	}

	public OrderAccessDeniedException(string message)
		: base(message)
	{
	}

	public OrderAccessDeniedException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
