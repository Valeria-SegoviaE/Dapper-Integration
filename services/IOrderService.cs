using Orders.Api.models;
using Orders.Api.models.dtos;

namespace Orders.Api.services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CreateOrderDto createOrderDto, CancellationToken cancellationToken);
        Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Order>> GetOrdersAsync(string? status, int? customerId, string? orderNumber, CancellationToken cancellationToken);
        Task UpdateOrderAsync(int orderId, UpdateOrderDto updateOrderDto, CancellationToken cancellationToken);
        Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken);

    }
}