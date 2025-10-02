using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetSocle.Service.Contracts;

public interface IServiceAsync<TQuery, TResult> : IDisposable, IAsyncDisposable where TResult : IServiceResult
{
    public Task<TResult> ExecuteAsync(TQuery query, CancellationToken ct = default);
}