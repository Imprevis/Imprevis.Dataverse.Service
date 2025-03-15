namespace Imprevis.Dataverse.Service.Extensions;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class DataverseServiceFactoryBuilderExtensions
{
    public static IDataverseServiceBuilder WithWarmupService(this IDataverseServiceBuilder builder)
    {
        builder.Services.AddHostedService<DataverseServiceWarmupService>();
        return builder;
    }
}
