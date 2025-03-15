namespace Imprevis.Dataverse.Service.Resolvers.Http;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByHeaderExtensions
{
    public static IDataverseServiceBuilder ResolveByHeader(this IDataverseServiceBuilder builder, string name, Func<string?, Guid?>? parse = null)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByHeader(httpContextAccessor, name, parse);
            return resolver;
        });

        return builder;
    }
}
