namespace Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.DependencyInjection;

internal class MockDataverseServiceBuilder(IServiceCollection services) : IDataverseServiceBuilder
{
    public IServiceCollection Services => services;
}

