using Microsoft.AspNetCore.Mvc;
using Orders.Api.models;
using Orders.Api.models.dtos;
using Orders.Api.services;

namespace Orders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders(
        [FromQuery] string? status,
        [FromQuery] int? customerId,
        [FromQuery] string? orderNumber,
        CancellationToken cancellationToken)
    {
        return Ok(await orderService.GetOrdersAsync(status, customerId, orderNumber, cancellationToken));
    }

    [HttpGet("{orderId:int}")]
    public async Task<ActionResult<Order>> GetOrder(int orderId, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderAsync(orderId, cancellationToken);
        return order is null ? NotFound(new { Message = "Order not found." }) : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        CreateOrderDto createOrderDto,
        CancellationToken cancellationToken)
    {
        var orderId = await orderService.CreateOrderAsync(createOrderDto, cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new { orderId }, new
        {
            Message = "Order created successfully.",
            OrderId = orderId
        });
    }

    [HttpGet("terminadas")]
    public async Task<ActionResult<IReadOnlyList<Order>>> GetFinishedOrders(CancellationToken cancellationToken)
    {
        return Ok(await orderService.GetOrdersAsync("Delivered", null, null, cancellationToken));
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalOrders(CancellationToken cancellationToken)
    {
        var orders = await orderService.GetOrdersAsync(null, null, null, cancellationToken);
        return Ok(new { totalOrders = orders.Count });
    }

    [HttpPut("actualizar/{orderId:int}")]
    public async Task<IActionResult> UpdateOrder(
        int orderId,
        UpdateOrderDto updateOrderDto,
        CancellationToken cancellationToken)
    {
        if (await orderService.GetOrderAsync(orderId, cancellationToken) is null)
            return NotFound(new { Message = "Order not found." });

        await orderService.UpdateOrderAsync(orderId, updateOrderDto, cancellationToken);
        var updatedOrder = await orderService.GetOrderAsync(orderId, cancellationToken);
        return Ok(new { Message = "Order updated successfully.", UpdatedOrder = updatedOrder });
    }

    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> DeleteOrder(int orderId, CancellationToken cancellationToken)
    {
        if (await orderService.GetOrderAsync(orderId, cancellationToken) is null)
            return NotFound(new { Message = "Order not found." });

        await orderService.DeleteOrderAsync(orderId, cancellationToken);
        return NoContent();
    }
}
