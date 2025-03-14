namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByRouteValueExtensions
{
    public static IDataverseServiceBuilder ResolveByRouteValue(this IDataverseServiceBuilder builder, string name, Func<object?, Guid?>? parse = null)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByRouteValue(httpContextAccessor, name, parse);
            return resolver;
        });

        return builder;
    }
}
