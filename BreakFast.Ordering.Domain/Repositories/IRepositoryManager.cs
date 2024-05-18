namespace BreakFast.Ordering.Domain.Repositories;

public interface IRepositoryManager
{
	IOrderRepository OrderRepository { get; }

	IUnitOfWork UnitOfWork { get; }
}
