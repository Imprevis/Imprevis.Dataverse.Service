namespace Imprevis.Dataverse.Service.Abstractions;

using Microsoft.Extensions.DependencyInjection;

public interface IDataverseServiceBuilder
{
    IServiceCollection Services { get; }
}
