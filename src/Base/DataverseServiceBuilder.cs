namespace Imprevis.Dataverse;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.DependencyInjection;

internal class DataverseServiceBuilder(IServiceCollection services) : IDataverseServiceBuilder
{
    public IServiceCollection Services => services;
}
