namespace Imprevis.Dataverse.Service;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Extensions.DependencyInjection;

internal class DataverseServiceBuilder(IServiceCollection services) : IDataverseServiceBuilder
{
    public IServiceCollection Services => services;
}
