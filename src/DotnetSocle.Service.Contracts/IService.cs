using System;

namespace DotnetSocle.Service.Contracts;

public interface IService<TQuery, TResult> : IDisposable where TResult : IServiceResult
{
    public TResult Execute(TQuery query);
}