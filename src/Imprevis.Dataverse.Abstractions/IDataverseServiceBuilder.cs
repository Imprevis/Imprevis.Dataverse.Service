namespace Imprevis.Dataverse.Abstractions;

using Microsoft.Extensions.DependencyInjection;

public interface IDataverseServiceBuilder
{
    IServiceCollection Services { get; }
}
