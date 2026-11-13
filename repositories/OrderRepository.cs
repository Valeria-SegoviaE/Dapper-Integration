using System.Data;
using Dapper;
using Orders.Api.models;
using Orders.Api.models.dtos;
using Orders.Api.services;

namespace Orders.Api.repositories;

public sealed class OrderRepository(IDapperService dapperService) : IOrderRepository
{
    public async Task<int> CreateAsync(CreateOrderDto order, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@OrderNumber", order.OrderNumber);
        parameters.Add("@CustomerId", order.CustomerId);
        parameters.Add("@VehicleId", order.VehicleId);
        parameters.Add("@OriginCity", order.OriginCity);
        parameters.Add("@DestinationCity", order.DestinationCity);
        parameters.Add("@CargoDescription", order.CargoDescription);
        parameters.Add("@WeightKg", order.WeightKg);
        parameters.Add("@FreightAmount", order.FreightAmount);
        parameters.Add("@CurrencyCode", order.CurrencyCode);
        parameters.Add("@Status", order.Status);
        parameters.Add("@PickupDate", order.PickupDate);
        parameters.Add("@DeliveryDate", order.DeliveryDate);
        parameters.Add("@Notes", order.Notes);
        parameters.Add("@OrderId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        return await dapperService.ExecuteWithOutputAsync<int>(
            "dbo.usp_TransportOrders_Create",
            parameters,
            "@OrderId",
            cancellationToken);
    }

    public Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken) =>
        dapperService.QuerySingleOrDefaultAsync<Order>(
            "dbo.usp_TransportOrders_GetById",
            new { OrderId = orderId },
            cancellationToken);

    public async Task<IReadOnlyList<Order>> GetAllAsync(
        string? status,
        int? customerId,
        string? orderNumber,
        CancellationToken cancellationToken)
    {
        var orders = await dapperService.QueryAsync<Order>(
            "dbo.usp_TransportOrders_GetAll",
            new { Status = status, CustomerId = customerId, OrderNumber = orderNumber },
            cancellationToken);
        return orders.ToList();
    }

    public Task UpdateAsync(int orderId, UpdateOrderDto order, CancellationToken cancellationToken) =>
        dapperService.ExecuteAsync(
            "dbo.usp_TransportOrders_Update",
            new
            {
                OrderId = orderId,
                order.OrderNumber,
                order.CustomerId,
                order.VehicleId,
                order.OriginCity,
                order.DestinationCity,
                order.CargoDescription,
                order.WeightKg,
                order.FreightAmount,
                order.CurrencyCode,
                order.Status,
                order.PickupDate,
                order.DeliveryDate,
                order.Notes
            },
            cancellationToken);

    public Task DeleteAsync(int orderId, CancellationToken cancellationToken) =>
        dapperService.ExecuteAsync(
            "dbo.usp_TransportOrders_Delete",
            new { OrderId = orderId },
            cancellationToken);
}
