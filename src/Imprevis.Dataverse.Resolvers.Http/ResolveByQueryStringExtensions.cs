namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByQueryStringExtensions
{
    public static IDataverseServiceBuilder ResolveByQueryString(this IDataverseServiceBuilder builder, string name, Func<string?, Guid?>? parse = null)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByQueryString(httpContextAccessor, name, parse);
            return resolver;
        });

        return builder;
    }
}
