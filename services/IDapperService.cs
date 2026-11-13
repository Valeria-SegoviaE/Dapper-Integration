using Dapper;

namespace Orders.Api.services;

public interface IDapperService
{
    Task<int> ExecuteAsync(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken);

    Task<IEnumerable<T>> QueryAsync<T>(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken);

    Task<T?> QuerySingleOrDefaultAsync<T>(
        string storedProcedure,
        object? parameters,
        CancellationToken cancellationToken);

    Task<T> ExecuteWithOutputAsync<T>(
        string storedProcedure,
        DynamicParameters parameters,
        string outputParameter,
        CancellationToken cancellationToken);
}
