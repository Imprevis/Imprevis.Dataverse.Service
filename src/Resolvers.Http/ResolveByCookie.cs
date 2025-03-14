namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;

public class ResolveByCookie(IHttpContextAccessor? httpContextAccessor, string name, Func<string?, Guid?>? parse = null) : IDataverseServiceResolver
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

        if (httpContext.Request.Cookies.ContainsKey(name))
        {
            var value = httpContext.Request.Cookies[name];

            // Use custom processing method
            if (parse != null)
            {
                return parse(value);
            }

            // Parse as a Guid
            var parsed = Guid.TryParse(value, out var organizationId);
            if (parsed)
            {
                return organizationId;
            }
        }

        return null;
    }
}
