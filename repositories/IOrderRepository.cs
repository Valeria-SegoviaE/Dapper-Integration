using Orders.Api.models;
using Orders.Api.models.dtos;

namespace Orders.Api.repositories;

public interface IOrderRepository
{
    Task<int> CreateAsync(CreateOrderDto order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Order>> GetAllAsync(string? status, int? customerId, string? orderNumber, CancellationToken cancellationToken);
    Task UpdateAsync(int orderId, UpdateOrderDto order, CancellationToken cancellationToken);
    Task DeleteAsync(int orderId, CancellationToken cancellationToken);
}
