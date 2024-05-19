using BreakFast.Ordering.Domain.Repositories;

namespace BreakFast.Ordering.Infrastructure.Repositories;

public class RepositoryManager(OrderingContext dbContext) : IRepositoryManager
{
	private readonly Lazy<IOrderRepository> _lazyOrderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(dbContext));
	private readonly Lazy<IUnitOfWork> _lazyUnitOfWork = new(() => new UnitOfWork(dbContext));

	public IOrderRepository OrderRepository => _lazyOrderRepository.Value;

	public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}
