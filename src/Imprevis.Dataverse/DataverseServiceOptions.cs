namespace Imprevis.Dataverse;

public class DataverseServiceOptions
{
    public Guid OrganizationId { get; }
    public string OrganizationName { get; } = string.Empty;
    public string ConnectionString { get; } = string.Empty;
}
