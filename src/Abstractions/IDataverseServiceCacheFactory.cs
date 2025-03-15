namespace Imprevis.Dataverse.Service.Abstractions;

public interface IDataverseServiceCacheFactory
{
    IDataverseServiceCache Create(Guid organizationId);
}
