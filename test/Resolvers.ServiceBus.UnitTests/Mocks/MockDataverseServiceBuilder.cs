namespace Imprevis.Dataverse.Service.Resolvers.ServiceBus.UnitTests.Mock;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Extensions.DependencyInjection;

internal class MockDataverseServiceBuilder(IServiceCollection services) : IDataverseServiceBuilder
{
    public IServiceCollection Services => services;
}

