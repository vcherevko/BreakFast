namespace BreakFast.Ordering.Domain.Repositories;

public interface IUnitOfWork
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
