namespace Imprevis.Dataverse;

public class DataverseServiceFactoryOptions
{
    public const string Section = "Dataverse";

    public Dictionary<string, string> ConnectionStrings { get; set; } = [];
}
