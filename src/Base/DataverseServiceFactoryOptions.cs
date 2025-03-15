namespace Imprevis.Dataverse.Service;

public class DataverseServiceFactoryOptions
{
    public const string Section = "Dataverse";

    public IEnumerable<DataverseServiceOptions> Services { get; set; } = [];
}
