using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DotnetSocle.Service;

public abstract class AbstractResult
{
    public virtual bool Success => Errors.Count.Equals(0);
    public ICollection<Validation> Errors { get; set; } = [];

    public void AddError(string title, string detail)
    {
        Errors ??= new Collection<Validation>();

        Errors.Add(new Validation
        {
            Title = title,
            Detail = detail,
        });
    }
}

public abstract class AbstractResult<T> : AbstractResult
{
    public override bool Success => base.Success && Data is not null;
    public required T Data { get; set; }
}