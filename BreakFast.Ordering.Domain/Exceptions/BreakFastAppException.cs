namespace BreakFast.Ordering.Domain.Exceptions;

public class BreakFastAppException : Exception
{
	public BreakFastAppException()
	{
	}

	public BreakFastAppException(string message)
		: base(message)
	{
	}

	public BreakFastAppException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
