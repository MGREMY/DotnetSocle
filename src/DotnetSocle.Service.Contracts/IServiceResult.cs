namespace DotnetSocle.Service.Contracts;

public interface IServiceResult
{
    public bool Success { get; }
    public ICollection<IServiceResultValidation> Errors { get; set; }

    public void AddError(string title, string detail);
}

public interface IServiceResult<T> : IServiceResult
{
    public T Data { get; set; }
}