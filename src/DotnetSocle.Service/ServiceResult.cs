using System.Collections.Generic;
using DotnetSocle.Service.Contracts;

namespace DotnetSocle.Service;

public class ServiceResult : IServiceResult
{
    public virtual bool Success => Errors.Count.Equals(0);
    public ICollection<IServiceResultValidation> Errors { get; set; } = [];

    public void AddError(string title, string detail)
    {
        Errors.Add(new ServiceValidation
        {
            Title = title,
            Detail = detail,
        });
    }
}

public class ServiceResult<T> : ServiceResult, IServiceResult<T>
{
    public override bool Success => base.Success && Data is not null;
    public T Data { get; set; }
}