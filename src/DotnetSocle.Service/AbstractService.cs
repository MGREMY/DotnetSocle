using System;
using Microsoft.Extensions.Logging;

namespace DotnetSocle.Service;

public abstract class AbstractService<TQuery, TResult> : IDisposable where TResult : AbstractResult, new()
{
    protected readonly ILogger<AbstractService<TQuery, TResult>> Logger;

    protected AbstractService(ILogger<AbstractService<TQuery, TResult>> logger)
    {
        Logger = logger;
    }

    protected abstract TResult Handle(TQuery query);

    protected virtual TResult PreExecute(TQuery query)
    {
        var result = new TResult();

        Logger.LogInformation("Start : {service} with {query}", this, query);

        return result;
    }

    protected virtual TResult PostExecute(TQuery query)
    {
        var result = new TResult();

        Logger.LogInformation("End : {service} with {query}", this, query);

        return result;
    }

    public virtual TResult Execute(TQuery query)
    {
        if (PreExecute(query) is { Success: false } preExecutionResult)
        {
            Logger.LogInformation(
                "End : {service} with {query} ; The PreExecute function return Success = false {result}",
                this,
                query,
                preExecutionResult);

            return preExecutionResult;
        }

        if (Handle(query) is { Success: false } executionResult)
        {
            Logger.LogInformation(
                "End : {service} with {query} ; The Handle function return Success = false {result}",
                this,
                query,
                executionResult);

            return executionResult;
        }

        return PostExecute(query);
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}