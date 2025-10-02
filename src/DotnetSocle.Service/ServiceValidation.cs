using DotnetSocle.Service.Contracts;

namespace DotnetSocle.Service;

public class ServiceValidation : IServiceResultValidation
{
    public required string Title { get; set; }
    public required string Detail { get; set; }
}