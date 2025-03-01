namespace Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests.Mocks;

using System;

internal static class ParseGuid
{
    public static Guid? Parse(string? value)
    {
        if (value == null)
        {
            return null;
        }

        var parsed = Guid.TryParse(value, out var organizationId);
        if (parsed)
        {
            return organizationId;
        }

        return null;
    }
}
