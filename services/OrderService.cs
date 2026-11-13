using Orders.Api.models.dtos;
using Orders.Api.models;
using Orders.Api.repositories;

namespace Orders.Api.services
{
    public sealed class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public Task<int> CreateOrderAsync(CreateOrderDto order, CancellationToken cancellationToken) =>
            orderRepository.CreateAsync(order, cancellationToken);

        public Task<Order?> GetOrderAsync(int orderId, CancellationToken cancellationToken) =>
            orderRepository.GetByIdAsync(orderId, cancellationToken);

        public Task<IReadOnlyList<Order>> GetOrdersAsync(
            string? status,
            int? customerId,
            string? orderNumber,
            CancellationToken cancellationToken) =>
            orderRepository.GetAllAsync(status, customerId, orderNumber, cancellationToken);

        public Task UpdateOrderAsync(int orderId, UpdateOrderDto order, CancellationToken cancellationToken) =>
            orderRepository.UpdateAsync(orderId, order, cancellationToken);

        public Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken) =>
            orderRepository.DeleteAsync(orderId, cancellationToken);
    }
}