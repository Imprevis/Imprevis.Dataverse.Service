namespace Imprevis.Dataverse.Service.Abstractions;

public interface IDataverseServiceFactory : IDisposable
{
    IDataverseService GetService(Func<IDataverseService, bool> predicate);
    IEnumerable<IDataverseService> GetServices(Func<IDataverseService, bool>? predicate = null);
}
