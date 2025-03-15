﻿namespace Imprevis.Dataverse.Service.Resolvers.Http;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.AspNetCore.Http;

public class ResolveByRouteValue(IHttpContextAccessor? httpContextAccessor, string name, Func<string?, Guid?>? parse = null) : IDataverseServiceResolver
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

        var success = httpContext.Request.RouteValues.TryGetValue(name, out var value);
        if (success && value != null)
        {
            // Use custom processing method
            if (parse != null)
            {
                return parse(value.ToString());
            }

            // Parse as a Guid
            var parsed = Guid.TryParse(value.ToString(), out var organizationId);
            if (parsed)
            {
                return organizationId;
            }
        }

        return null;
    }
}
