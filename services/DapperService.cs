using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Orders.Api.services;

public sealed class DapperService(IConfiguration configuration) : IDapperService
{
    private readonly string _connectionString = GetConnectionString(configuration);

    public async Task<int> ExecuteAsync(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        var command = new CommandDefinition(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        var command = new CommandDefinition(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);
        return await connection.QueryAsync<T>(command);
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        var command = new CommandDefinition(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure,
            cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<T>(command);
    }

    public async Task<T> ExecuteWithOutputAsync<T>(
        string storedProcedure,
        DynamicParameters parameters,
        string outputParameter,
        CancellationToken cancellationToken)
    {
        await ExecuteAsync(storedProcedure, parameters, cancellationToken);
        return parameters.Get<T>(outputParameter);
    }

    private static string GetConnectionString(IConfiguration configuration) =>
        !string.IsNullOrWhiteSpace(configuration.GetConnectionString("TransportOrders"))
            ? configuration.GetConnectionString("TransportOrders")!
            : throw new InvalidOperationException(
                "The TransportOrders connection string is not configured. Set it in user-secrets.");
}
