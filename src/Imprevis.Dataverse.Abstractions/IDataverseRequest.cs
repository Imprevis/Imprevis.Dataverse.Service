namespace Imprevis.Dataverse.Abstractions;

using Microsoft.Extensions.Logging;

public interface IDataverseRequest
{
    Task Execute(IDataverseService service, ILogger logger, CancellationToken cancellationToken = default);
}

public interface IDataverseRequest<TResponse>
{
    Task<TResponse> Execute(IDataverseService service, ILogger logger, CancellationToken cancellationToken = default);
}
