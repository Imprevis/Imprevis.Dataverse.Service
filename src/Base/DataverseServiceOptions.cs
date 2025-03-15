namespace Imprevis.Dataverse.Service;

public class DataverseServiceOptions
{
    public required Guid OrganizationId { get; set; }
    public required string OrganizationName { get; set; }
    public required string ConnectionString { get; set; }
}
