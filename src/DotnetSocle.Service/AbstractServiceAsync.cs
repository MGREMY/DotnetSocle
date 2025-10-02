using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DotnetSocle.Service;

public abstract class AbstractServiceAsync<TQuery, TResult> : IDisposable, IAsyncDisposable where TResult : AbstractResult, new()
{
    protected readonly ILogger<AbstractServiceAsync<TQuery, TResult>> Logger;

    protected AbstractServiceAsync(ILogger<AbstractServiceAsync<TQuery, TResult>> logger)
    {
        Logger = logger;
    }
    
    protected abstract Task<TResult> HandleAsync(TQuery query, CancellationToken ct = default);

    protected virtual Task<TResult> PreExecuteAsync(TQuery query, CancellationToken ct = default)
    {
        var result = new TResult();

        Logger.LogInformation("Start : {service} with {query}", this, query);

        return Task.FromResult(result);
    }

    protected virtual Task<TResult> PostExecuteAsync(TQuery query, CancellationToken ct = default)
    {
        var result = new TResult();

        Logger.LogInformation("End : {service} with {query}", this, query);

        return Task.FromResult(result);
    }

    public virtual async Task<TResult> ExecuteAsync(TQuery query, CancellationToken ct = default)
    {
        if (await PreExecuteAsync(query, ct) is { Success: false } preExecutionResult)
        {
            Logger.LogInformation(
                "End : {service} with {query} ; The PreExecute function return Success = false {result}",
                this,
                query,
                preExecutionResult);

            return preExecutionResult;
        }

        if (await HandleAsync(query, ct) is { Success: false } executionResult)
        {
            Logger.LogInformation(
                "End : {service} with {query} ; The Handle function return Success = false {result}",
                this,
                query,
                executionResult);

            return executionResult;
        }

        return await PostExecuteAsync(query, ct);
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public virtual ValueTask DisposeAsync()
    {
        Dispose();
        GC.SuppressFinalize(this);

        return ValueTask.CompletedTask;
    }
}
