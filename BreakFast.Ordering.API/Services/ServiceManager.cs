using BreakFast.Ordering.Domain.Repositories;

namespace BreakFast.Ordering.API.Services;

public class ServiceManager : IServiceManager
{
	private readonly Lazy<IOrderService> _lazyConsumerService;

	public ServiceManager(IRepositoryManager repositoryManager)
	{
		_lazyConsumerService = new Lazy<IOrderService>(() => new OrderService(repositoryManager));
	}

	public IOrderService OrderService => _lazyConsumerService.Value;
}
