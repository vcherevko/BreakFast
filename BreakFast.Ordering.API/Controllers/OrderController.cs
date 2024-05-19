using BreakFast.Ordering.API.DTOs;
using BreakFast.Ordering.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreakFast.Ordering.API.Controllers;

[ApiController]
[Route("api/v1/order/")]
public class OrderController(
	ILogger<OrderController> logger,
	IServiceManager serviceManager)
	: ControllerBase
{
	private readonly ILogger<OrderController> _logger = logger;
	private readonly IOrderService _consumerService = serviceManager.OrderService;

/// <summary>
/// Get all user's orders
/// </summary>
/// <param name="consumerId"></param>
/// <param name="cancellationToken"></param>
/// <returns>The list of all orders</returns>
/// <response code="200">Returning list of orders</response>
/// <response code="404">Bad request</response>
/// <exception cref="Exception"></exception>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
	public async Task<IActionResult> GetOrdersByUserAsync(int consumerId, CancellationToken cancellationToken)
	{
		return Ok(await _consumerService.GetOrdersAsync(consumerId, cancellationToken));
	}

	/// <summary>
	/// Gives an order by Id
	/// </summary>
	/// <remarks>
	/// If user doesn't have access to an order, then an exception will be thrown
	/// </remarks>
	/// <param name="consumerId"></param>
	/// <param name="orderId"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>The list of all orders</returns>
	/// <response code="200">Returning list of orders</response>
	/// <response code="403">Request is forbidden for this consumer</response>
	/// <response code="404">Bad request</response>
	/// <exception cref="Exception"></exception>
	[HttpGet("{orderId:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
	[ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
	public async Task<IActionResult> GetOrderAsync(int consumerId, int orderId, CancellationToken cancellationToken)
	{
		return Ok(await _consumerService.GetOrderByIdAsync(consumerId, orderId, cancellationToken));
	}

	/// <summary>
	/// Creates new order
	/// </summary>
	/// <param name="consumerId"></param>
	/// <param name="orderModel"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>The list of all orders</returns>
	/// <response code="201">Order created</response>
	/// <response code="404">Bad request</response>
	/// <exception cref="Exception"></exception>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
	public async Task<IActionResult> CreateOrderAsync(int consumerId, [FromBody]OrderCreatingDto orderModel, CancellationToken cancellationToken)
	{
		await _consumerService.CreateOrderAsync(consumerId, orderModel, cancellationToken);
		return Created();
	}
}
