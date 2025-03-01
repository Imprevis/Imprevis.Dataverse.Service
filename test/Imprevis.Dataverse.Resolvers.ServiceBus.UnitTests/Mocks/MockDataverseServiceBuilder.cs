namespace Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests.Mock;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.DependencyInjection;

internal class MockDataverseServiceBuilder(IServiceCollection services) : IDataverseServiceBuilder
{
    public IServiceCollection Services => services;
}

