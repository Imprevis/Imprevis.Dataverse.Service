namespace Imprevis.Dataverse.Service.Extensions;

using Imprevis.Dataverse.Service.Abstractions;

public static class DataverseServiceFactoryExtensions
{
    public static IDataverseService GetService(this IDataverseServiceFactory factory, Guid organizationId)
    {
        return factory.GetService(x => x.OrganizationId == organizationId);
    }

    public static IDataverseService GetService(this IDataverseServiceFactory factory, string organizationName)
    {
        return factory.GetService(x => x.OrganizationName == organizationName);
    }
}
