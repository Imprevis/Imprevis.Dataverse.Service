namespace Imprevis.Dataverse.Service;

public class DataverseServiceOptions
{
    /// <summary>
    /// The Organization ID of the service.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Friendly name of the service.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The URL of the service.
    /// </summary>
    /// <remarks>
    /// This is required if ConnectionString is not provided.
    /// </remarks>
    public string? Url { get; set; }

    /// <summary>
    /// The Client ID used to connect to the service.
    /// </summary>
    /// <remarks>
    /// This is required if ConnectionString is not provided.
    /// </remarks>
    public string? ClientId { get; set; }

    /// <summary>
    /// The Client Secret used to connect to the service.
    /// </summary>
    /// <remarks>
    /// This is required if ConnectionString is not provided.
    /// </remarks>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The Client ID used to connect to the service.
    /// </summary>
    /// <remarks>
    /// This is required if Url, ClientId, and ClientSecret are not provided.
    /// </remarks>
    public string? ConnectionString { get; set; }
}
