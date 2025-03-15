namespace Imprevis.Dataverse.Service.Resolvers.Http;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByBodyExtensions
{
    public static IDataverseServiceBuilder ResolveByBody(this IDataverseServiceBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByBody(httpContextAccessor);
            return resolver;
        });

        return builder;
    }

    public static IDataverseServiceBuilder ResolveByBody<TRequest>(this IDataverseServiceBuilder builder, Func<TRequest?, Guid?> parse)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByBody<TRequest>(httpContextAccessor, parse);
            return resolver;
        });

        return builder;
    }
}
