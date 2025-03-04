namespace Imprevis.Dataverse;

public class DataverseServiceFactoryOptions
{
    public const string Section = "Dataverse";

    public IEnumerable<DataverseServiceOptions> Services { get; set; } = [];
}
