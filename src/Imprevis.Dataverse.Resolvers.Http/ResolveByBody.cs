namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public class ResolveByBody(IHttpContextAccessor? httpContextAccessor) : IDataverseServiceResolver
{
    public Guid? Resolve()
    {
        static Guid? parser(string? value)
        {
            var parsed = Guid.TryParse(value, out var organizationId);
            if (parsed)
            {
                return organizationId;
            }

            return null;
        }

        return new ResolveByBody<string>(httpContextAccessor, parser).Resolve();
    }
}

public class ResolveByBody<TRequest>(IHttpContextAccessor? httpContextAccessor, Func<TRequest?, Guid?> parse) : IDataverseServiceResolver
{
    public Guid? Resolve()
    {
        if (httpContextAccessor == null)
        {
            return null;
        }

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return null;
        }

        try
        {
            httpContext.Request.Body.Position = 0;

            var body = JsonSerializer.Deserialize<TRequest>(httpContext.Request.Body);
            return parse(body);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
