using BreakFast.Ordering.Domain.Repositories;

namespace BreakFast.Ordering.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly OrderingContext _dbContext;

	public UnitOfWork(OrderingContext dbContext)
	{
		_dbContext = dbContext;
	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return _dbContext.SaveChangesAsync(cancellationToken);
	}
}
