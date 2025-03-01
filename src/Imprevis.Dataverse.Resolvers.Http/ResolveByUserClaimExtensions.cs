namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByUserClaimExtensions
{
    public static IDataverseServiceBuilder ResolveByUserClaim(this IDataverseServiceBuilder builder, string type, Func<string?, Guid?>? parse = null)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            var resolver = new ResolveByUserClaim(httpContextAccessor, type, parse);
            return resolver;
        });

        return builder;
    }
}
